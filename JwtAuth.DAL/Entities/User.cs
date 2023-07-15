using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuth.DAL.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
