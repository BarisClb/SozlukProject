using SozlukProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Service.Contracts
{
    public interface IJwt
    {
        string GenerateJwt(BaseEntity account);
        JwtSecurityToken Verify(string jwt);
    }
}
