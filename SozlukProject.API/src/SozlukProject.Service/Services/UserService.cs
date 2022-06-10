using AutoMapper;
using SozlukProject.Domain.Entities;
using SozlukProject.Domain.Repositories;
using SozlukProject.Domain.Responses;
using SozlukProject.Domain.Validations;
using SozlukProject.Domain.Validations.Common;
using SozlukProject.Service.Contracts;
using SozlukProject.Service.Dtos.Common;
using SozlukProject.Service.Dtos.Create;
using SozlukProject.Service.Dtos.Read;
using SozlukProject.Service.Dtos.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Services
{
    public class UserService : BaseService<User, UserCreateDto, UserUpdateDto, UserReadDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        private readonly IVoteRepository _voteRepository;
        private readonly IBCryptNet _bCryptNet;

        public UserService(IUserRepository userRepository, IMapper mapper, IEmailService emailService, IVoteRepository voteRepository, IBCryptNet bCryptNet) : base(userRepository, mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _emailService = emailService;

            _voteRepository = voteRepository;
            _bCryptNet = bCryptNet;
        }


        public async Task<BaseResponse> GetSortedUserList(EntityListSortValues sortValues, string? byEntity = null)
        {
            // Check if there is a SearchWord, create a Predicate if there is one.
            Expression<Func<User, bool>>? searchWordPredicate = null;
            if (!string.IsNullOrWhiteSpace(sortValues.SearchWord))
                searchWordPredicate = user => user.Username.Contains(sortValues.SearchWord);

            // Check if there is an OrderBy in sortValues, if there is, implement it. If not, use the default 'Id'
            //Expression<Func<User, string>> orderByPredicate = c => c.UserId;
            Expression<Func<User, object>>? orderByKeySelector = sortValues.OrderBy switch
            {
                "Name" => user => user.Name,
                "Username" => user => user.Username,
                "Email" => user => user.Email,
                _ => null,
            };


            return await GetSortedEntityList(sortValues, searchWordPredicate, orderByKeySelector);
        }

        public async Task<BaseResponse> CreateUser(UserCreateDto userCreateDto)
        {
            // Check Name
            userCreateDto.Name = CommonValidator.TrimAndCheckIfEmpty(userCreateDto.Name, "Name");

            // Check Username
            userCreateDto.Username = CommonValidator.TrimAndCheckIfEmpty(userCreateDto.Username, "Username");
            AccountValidator.CheckUsername(userCreateDto.Username);
            if (await _userRepository.GetSingleAsync(user => user.Username == userCreateDto.Username) != null)
                return new FailResponse("Username already exists.");

            // Check Email
            userCreateDto.Email = CommonValidator.TrimAndCheckIfEmpty(userCreateDto.Email, "Email");
            AccountValidator.CheckEmail(userCreateDto.Email);
            if (await _userRepository.GetSingleAsync(user => user.Email == userCreateDto.Email) != null)
                return new FailResponse("Email already exists.");

            // Check Password
            userCreateDto.Password = CommonValidator.TrimAndCheckIfEmpty(userCreateDto.Password, "Password");
            AccountValidator.CheckPassword(userCreateDto.Password);
            userCreateDto.Password = _bCryptNet.HashPassword(userCreateDto.Password);

            // Admin Password
            if (userCreateDto.AdminPassword != null)
            {
                // Check if Password matches
                if (AccountValidator.CheckAdminPassword(userCreateDto.AdminPassword))
                    userCreateDto.Admin = true;
            }

            // Intercept response to send Email
            SuccessfulResponse<UserReadDto> response = await CreateEntity(userCreateDto);
            await _emailService.WelcomeEmail(response.Data);


            return response;
        }

        public async Task<BaseResponse> UpdateUser(UserUpdateDto userUpdateDto)
        {
            // First of all, we get and check if entity exist
            User user = await GetAndCheckEntityById(userUpdateDto.Id);

            //// Then we move on the the properties
            // Name
            if (userUpdateDto.Name != null)
            {
                userUpdateDto.Name = CommonValidator.TrimAndCheckIfEmpty(userUpdateDto.Name, "Name");

                user.Name = userUpdateDto.Name;
            }
            // Username
            if (userUpdateDto.Username != null)
            {
                userUpdateDto.Username = CommonValidator.TrimAndCheckIfEmpty(userUpdateDto.Username, "Username");
                // Check if Username has any spaces between
                AccountValidator.CheckUsername(userUpdateDto.Username);
                if (await _userRepository.GetSingleAsync(user => user.Username == userUpdateDto.Username) != null)
                    return new FailResponse("Username already exists.");

                user.Username = userUpdateDto.Username;
            }
            // Email
            if (userUpdateDto.Email != null)
            {
                userUpdateDto.Email = CommonValidator.TrimAndCheckIfEmpty(userUpdateDto.Email, "Email");
                // Checking Email format
                AccountValidator.CheckEmail(userUpdateDto.Email);
                if (await _userRepository.GetSingleAsync(user => user.Email == userUpdateDto.Email) != null)
                    return new FailResponse("Email already exists.");

                user.Email = userUpdateDto.Email;
            }
            // Password
            if (userUpdateDto.Password != null)
            {
                userUpdateDto.Password = CommonValidator.TrimAndCheckIfEmpty(userUpdateDto.Password, "Password");
                // Checking Password format
                AccountValidator.CheckPassword(userUpdateDto.Password);

                // Hashing Password
                user.Password = _bCryptNet.HashPassword(userUpdateDto.Password);
            }
            // Admin Password
            if (userUpdateDto.AdminPassword != null)
            {
                // Check if Password matches
                if (AccountValidator.CheckAdminPassword(userUpdateDto.AdminPassword))
                    user.Admin = true;
            }
            // Admin
            if (userUpdateDto.Admin != null)
                user.Admin = (bool)userUpdateDto.Admin;

            // Active
            if (userUpdateDto.Active != null)
                user.Active = (bool)userUpdateDto.Active;

            // Banned
            if (userUpdateDto.Banned != null)
                user.Banned = (bool)userUpdateDto.Banned;


            return await UpdateEntity(user);
        }

        public async Task<BaseResponse> DeleteUser(int userId)
        {
            // EFCore Multiple Cascade Path problem between User-Comment-Vote
            // To solve this, I disabled the User -> Vote deletion. We will do it Manually first:
            await _voteRepository.DeleteWhereAsync(vote => vote.UserId == userId);

            // After that, we will delete the User.
            return (await DeleteEntity(userId));
        }
    }
}
