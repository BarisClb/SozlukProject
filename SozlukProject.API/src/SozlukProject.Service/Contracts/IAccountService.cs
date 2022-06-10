using SozlukProject.Domain.Entities;
using SozlukProject.Domain.Responses;
using SozlukProject.Service.Dtos.Create;
using SozlukProject.Service.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Contracts
{
    public interface IAccountService
    {
        Task<BaseResponse> Login(AccountLoginInfo accountLoginInfo, int? userId);
        Task<BaseResponse> Logout();
        Task<BaseResponse> Verify(JwtSecurityToken token);
        Task SendActivationCode(string? jwt);
        Task<BaseResponse> ActivateAccount(string? jwt, int activationCode);
    }
}
