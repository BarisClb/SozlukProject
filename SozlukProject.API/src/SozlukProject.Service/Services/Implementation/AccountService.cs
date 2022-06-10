using SozlukProject.Domain.Entities;
using SozlukProject.Domain.Responses;
using SozlukProject.Domain.Validations;
using SozlukProject.Service.Contracts;
using SozlukProject.Service.Dtos.Create;
using SozlukProject.Service.Dtos.Read;
using SozlukProject.Service.Dtos.Shared;
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

        private readonly IEmailService _emailService;
        private readonly IJwtService _jwtService;

        public AccountService(UserService userService, UserActivationService userActivationService, IEmailService emailService, IJwtService jwtService)
        {
            _userService = userService;
            _userActivationService = userActivationService;

            _emailService = emailService;
            _jwtService = jwtService;
        }

        public async Task SendActivationCode(string? jwt)
        {
            JwtSecurityToken token = _jwtService.Verify(jwt);
            UserReadDto user = await _userService.GetAndCheckEntityByIdDto(int.Parse(token.Issuer));


            await _emailService.ActivationEmail(user);
        }

        public async Task<BaseResponse> Login(AccountLoginInfo accountLoginInfo, int? userId = null)
        {
            UserReadDto user;

            // This check is for Logging in after a Register action. It might be too costly and I might need to seperate this first step into two different methods
            if (userId != null)
            {
                user = await _userService.GetAndCheckEntityByIdDto((int)userId);
            }
            else
            {
                if (AccountValidator.CheckEmail(accountLoginInfo.Account))
                    user = await _userService.GetEntityWhereDto(user => user.Email == accountLoginInfo.Account);
                else
                    user = await _userService.GetEntityWhereDto(user => user.Username == accountLoginInfo.Account);
            }


            throw new NotImplementedException();
        }

        public async Task<BaseResponse> Verify(JwtSecurityToken token)
        {
            int accountId = int.Parse(token.Issuer);


            return await _userService.GetEntityById(accountId);
        }

        public async Task<BaseResponse> ActivateAccount(string? jwt, int activationCode)
        {





            return new SuccessfulResponse<UserReadDto>("hello");
        }

        public async Task<BaseResponse> Logout()
        {
            // We do the Logout actions inside controller but I added this here as a method, in case we change the process in the future.
            return new FailResponse("You should not be able to see this.");
        }

        public async Task<BaseResponse> CreateUser(UserCreateDto userCreateDto)
        {
            // We intercept the response here to Automatically Login the User and also send an Activation Email.
            SuccessfulResponse<UserReadDto> response = (SuccessfulResponse<UserReadDto>)await _userService.CreateUser(userCreateDto);






            return response;
        }
    }
}
