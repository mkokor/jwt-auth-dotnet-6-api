﻿using JwtAuth.DAL.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JwtAuth.BLL.Utilities.TokenGenerationService
{
    public class TokenGenerationService : ITokenGenerationService
    {
        private readonly IConfiguration _configuration;

        public TokenGenerationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region JsonWebToken
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
            return new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, $"{user.UserId}"), // Subject identifier will be user identification number!
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

        #region RefreshToken
        public RefreshToken GenerateRefreshToken(int ownerId)
        {
            return new RefreshToken
            {
                Value = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                CreatedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddDays(7),
                OwnerId = ownerId
            };
        }
        #endregion
    }
}