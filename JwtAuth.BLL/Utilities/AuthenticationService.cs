using JwtAuth.BLL.Interfaces;
using JwtAuth.DAL.Entities;
using JwtAuth.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JwtAuth.BLL.Utilities
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }


        public void EncodePlaintextPassword(string plaintextPassword, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(plaintextPassword));
            }
        }

        public void ValidatePasswordStrength(string password)
        {
            // minimum 8 characters
            // minimum one digit
            // minimum one special character
            if (Regex.IsMatch(password, "^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{8,}$"))
                return;
            throw new Exception("Password needs to contain minimum of 8 characters, one digit and one special character!");
        }

        public async Task CheckUsernameAvailability(string username)
        {
            var allUsers = await _unitOfWork.UserRepository.GetAllUsers();
            try
            {
                allUsers.Find(user => user.Username.Equals(username));
                throw new Exception("Provided username is not available!");
            }
            catch (ArgumentNullException) { }
        }

        // Function below checks if provided plaintext password matches with provided hashed password (with specific salt)!
        public void ValidatePasswordHash(string plaintextPassword, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(plaintextPassword));
                if (computedPasswordHash.SequenceEqual(passwordHash))
                    return;
                throw new Exception("Password does not match the username!");
            }
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
                new Claim(ClaimTypes.Name, user.Username)
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