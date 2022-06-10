using SozlukProject.Domain.Entities;
using SozlukProject.Domain.Responses;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Contracts
{
    public interface IJwtService
    {
        string GenerateJwt(int id);
        JwtSecurityToken Verify(string jwt);
    }
}
