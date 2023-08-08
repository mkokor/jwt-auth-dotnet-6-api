using JwtAuth.DAL.Entities;
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
        private SymmetricSecurityKey GetSecretKey(string secret)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        }

        private SigningCredentials GetDigitalSignature(string secret)
        {
            // Algorithm is HMAC (symmetric), so the key is secret (not private).
            return new SigningCredentials(GetSecretKey(secret), SecurityAlgorithms.HmacSha512Signature);
        }

        private JwtSecurityToken ConfigureJwt(List<Claim> claims, string secret)
        {
            return new JwtSecurityToken(
                issuer: _configuration["JwtConfiguration:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: GetDigitalSignature(secret));
        }


        private string GenerateJwt(List<Claim> claims, string secret)
        {
            return new JwtSecurityTokenHandler().WriteToken(ConfigureJwt(claims, secret));
        }
        #endregion

        #region AccessToken
        private List<Claim> GetAccessTokenClaims(User user)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, $"{user.UserId}"), // Subject identifier will be user identification number!
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };
        }

        public string GenerateAccessToken(User user)
        {
            var secret = _configuration["JwtConfiguration:AccessToken:Secret"];
            return GenerateJwt(GetAccessTokenClaims(user), secret);
        }
        #endregion

        #region RefreshToken
        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
        #endregion
    }
}