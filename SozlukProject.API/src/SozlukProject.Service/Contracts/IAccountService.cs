using SozlukProject.Domain.Responses;
using SozlukProject.Service.Dtos.Account;
using SozlukProject.Service.Dtos.Read;
using SozlukProject.Service.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Contracts
{
    public interface IAccountService
    {
        Task<LoginResponse<UserReadDto>> Login(AccountLoginInfoDto accountLoginInfo);
        Task<FailResponse> Logout();
        Task<SuccessfulResponse<UserReadDto>> Verify(string? jwt);
        Task<SuccessfulResponse<UserReadDto>> SendActivationCode(string? jwt);
        Task<SuccessfulResponse<UserReadDto>> ActivateAccount(string? jwt, int activationCode);
        Task<SuccessfulResponse<UserReadDto>> ChangePassword(AccountChangePasswordDto accountChangePasswordDto);
        Task<SuccessfulResponse<UserReadDto>> ForgotPassword(string email);
        Task<SuccessfulResponse<UserReadDto>> ResetPassword(AccountResetPasswordDto accountResetPasswordDto);
    }
}
