using AutoMapper;
using SozlukProject.Domain.Entities;
using SozlukProject.Domain.Responses;
using SozlukProject.Domain.Validations;
using SozlukProject.Service.Contracts;
using SozlukProject.Service.Dtos.Account;
using SozlukProject.Service.Dtos.Create;
using SozlukProject.Service.Dtos.Read;
using SozlukProject.Service.Dtos.Response;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly UserService _userService;
        private readonly UserActivationService _userActivationService;

        private readonly IBCryptNet _bCryptNet;
        private readonly IEmailService _emailService;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public AccountService(UserService userService, UserActivationService userActivationService, IBCryptNet bCryptNet, IEmailService emailService, IMapper mapper, IJwtService jwtService)
        {
            _userService = userService;
            _userActivationService = userActivationService;

            _bCryptNet = bCryptNet;
            _emailService = emailService;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<LoginResponse<UserReadDto>> Login(AccountLoginInfoDto accountLoginInfo)
        {
            // We use the Helper method to bring the User with Login info, then use mapper to turn it into UserReadDto
            UserReadDto user = _mapper.Map<User, UserReadDto>(await LoginHelper(accountLoginInfo.Account, accountLoginInfo.Password));

            // Generate Jwt and add it to response
            string jwt = await _jwtService.GenerateJwt(user.Id);


            return new LoginResponse<UserReadDto>(user, jwt);
        }

        public async Task<FailResponse> Logout()
        {
            // We do the Logout actions inside controller but I added this here as a method, in case we change the process in the future.
            return new FailResponse("You should not be able to see this.");
        }

        public async Task<SuccessfulResponse<UserReadDto>> Verify(string? jwt)
        {
            UserReadDto user = await IdentifyJwt(jwt);


            return new SuccessfulResponse<UserReadDto>(user);
        }

        public async Task<SuccessfulResponse<UserReadDto>> SendActivationCode(string? jwt)
        {
            // Get the User with Jwt
            UserReadDto user = await IdentifyJwt(jwt);
            if (user.Active)
                throw new Exception("Account is already Active.");

            // Send Activation Mail and get ActivationCode
            int activationCode = await _emailService.ActivationEmail(user);

            // UserActivation
            UserActivation userActivation = await _userActivationService.GetAndCheckEntityById(user.Id, "UserActivation");

            // Update UserActivation
            userActivation.ActivationCode = activationCode;
            await _userActivationService.UpdateEntity(userActivation);



            return new SuccessfulResponse<UserReadDto>("Activation code sent.");
        }

        public async Task<SuccessfulResponse<UserReadDto>> ActivateAccount(string? jwt, int activationCode)
        {
            // Get the User with Jwt, check if it's already Active
            UserReadDto userReadDto = await IdentifyJwt(jwt);
            if (userReadDto.Active)
                throw new Exception("Account is already Active.");

            // Get the UserActivation, if ActivationCode is correct, Update the User
            UserActivation userActivation = await _userActivationService.GetAndCheckEntityById(userReadDto.Id, "UserActivation");
            if (activationCode == userActivation.ActivationCode)
            {
                User user = await _userService.GetAndCheckEntityById(userReadDto.Id, "User");
                user.Active = true;


                return await _userService.UpdateEntity(user);
            }


            throw new Exception("Activation failed.");
        }

        public async Task<SuccessfulResponse<UserReadDto>> CreateUser(UserCreateDto userCreateDto)
        {
            // Create the User then intercept the response here to Automatically Login the User and also send an Activation Email.
            UserReadDto userReadDto = await _userService.CreateUser(userCreateDto);

            // Sending Welcome and Activation Email
            int activationCode = await _emailService.WelcomeEmail(userReadDto);

            // We create the UserActivation
            await _userActivationService.CreateEntity(new() { Id = userReadDto.Id, ActivationCode = activationCode, ResetPasswordCode = null });


            return new SuccessfulResponse<UserReadDto>(userReadDto);
        }

        public async Task<SuccessfulResponse<UserReadDto>> ChangePassword(AccountChangePasswordDto accountChangePasswordDto)
        {
            User user = await LoginHelper(accountChangePasswordDto.Account, accountChangePasswordDto.OldPassword);
            // If it doesn't return Exception, we hash and change the password
            user.Password = _bCryptNet.HashPassword(accountChangePasswordDto.NewPassword);


            // Update the User and return the response
            return await _userService.UpdateEntity(user);
        }

        public async Task<SuccessfulResponse<UserReadDto>> ForgotPassword(string email)
        {
            // Send ResetPasswordCode to Email and get it after
            int resetPasswordCode = await _emailService.ForgotPassword(email);

            // Get the User from Email, Get the UserActivation from UserId
            User user = await _userService.GetEntityWhere(u => u.Email == email);
            UserActivation userActivation = await _userActivationService.GetAndCheckEntityById(user.Id, "User");

            // Update the UserActivation
            userActivation.ResetPasswordCode = resetPasswordCode;
            await _userActivationService.UpdateEntity(userActivation);


            return new SuccessfulResponse<UserReadDto>("Password reset Email sent.");
        }

        public async Task<SuccessfulResponse<UserReadDto>> ResetPassword(AccountResetPasswordDto accountResetPasswordDto)
        {
            // Get the User from Email, Get the UserActivation from UserId
            User user = await _userService.GetEntityWhere(u => u.Email == accountResetPasswordDto.Email);
            UserActivation userActivation = await _userActivationService.GetAndCheckEntityById(user.Id, "User");

            if (accountResetPasswordDto.ForgotPasswordCode == userActivation.ResetPasswordCode)
            {
                user.Password = _bCryptNet.HashPassword(accountResetPasswordDto.NewPassword);


                return await _userService.UpdateEntity(user);
            }


            throw new Exception("Password reset failed");
        }


        // Helpers

        public async Task<UserReadDto> IdentifyJwt(string? jwt)
        {
            if (jwt == null)
                throw new Exception("Jwt does not exist.");

            JwtSecurityToken token = await _jwtService.Verify(jwt);
            UserReadDto user = await _userService.GetAndCheckEntityByIdDto(int.Parse(token.Issuer));

            return user;
        }

        public async Task<User> LoginHelper(string account, string password)
        {
            User user;

            if (AccountValidator.EmailFormat(account))
                user = await _userService.GetEntityWhere(user => user.Email == account);
            else
                user = await _userService.GetEntityWhere(user => user.Username == account);
            
            // Check if Password is Incorrect
            if (!_bCryptNet.CheckPassword(password, user.Password))
                throw new Exception("Login failed. (Incorrect password).");


            return user;
        }
    }
}
