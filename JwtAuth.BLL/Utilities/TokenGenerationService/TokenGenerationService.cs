using JwtAuth.DAL.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuth.BLL.Utilities.TokenGenerationService
{
    public class TokenGenerationService : ITokenGenerationService
    {
        private readonly IConfiguration _configuration;

        public TokenGenerationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region JSON Web Token
        private SymmetricSecurityKey GetSecretKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfiguration:Secret"]));
        }

        private SigningCredentials GetDigitalSignature()
        {
            // Algorithm is HMAC (symmetric), so the key is secret (not private).
            return new SigningCredentials(GetSecretKey(), SecurityAlgorithms.HmacSha512Signature);
        }

        private List<Claim> GetClaims(User user)
        {
            return new List<Claim>()
            {
                new Claim("sub", $"{user.UserId}"),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };
        }

        private JwtSecurityToken ConfigureJwt(User user)
        {
            return new JwtSecurityToken(
                issuer: _configuration["JwtConfiguration:Issuer"],
                claims: GetClaims(user),
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: GetDigitalSignature());
        }


        public string GenerateJwt(User user)
        {
            return new JwtSecurityTokenHandler().WriteToken(ConfigureJwt(user));
        }
        #endregion
    }
}