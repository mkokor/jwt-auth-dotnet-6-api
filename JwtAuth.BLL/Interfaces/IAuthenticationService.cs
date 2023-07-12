using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuth.BLL.Interfaces
{
    public interface IAuthenticationService
    {
        void EncodePlaintextPassword(string plaintextPassword, out byte[] passwordHash, out byte[] passwordSalt);

        void ValidatePasswordStrength(string password);

        Task CheckUsernameAvailability(string username);
    }
}
