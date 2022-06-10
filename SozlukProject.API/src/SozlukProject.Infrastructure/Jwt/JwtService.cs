using Microsoft.IdentityModel.Tokens;
using SozlukProject.Domain.Entities;
using SozlukProject.Domain.Responses;
using SozlukProject.Service.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SozlukProject.Infrastructure.Jwt
{
    public class JwtService : IJwtService
    {
        private static readonly string secureKey = "I wrote this text to set a SecureKey to implement Jwt!";

        public string GenerateJwt(int id)
        {
            SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(secureKey));
            SigningCredentials credentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtHeader header = new(credentials);

            JwtPayload payload = new(id.ToString(), null, null, null, DateTime.UtcNow.AddHours(1));
            JwtSecurityToken securityToken = new(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public JwtSecurityToken Verify(string jwt)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(secureKey);

            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
    }
}
