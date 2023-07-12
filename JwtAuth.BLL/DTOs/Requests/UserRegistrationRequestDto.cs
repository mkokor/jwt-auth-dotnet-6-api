using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuth.BLL.DTOs.Requests
{
    public class UserRegistrationRequestDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // Different role registration logic should be implemented for real world purposes.
        // User should not be able to define his own role.
        // This logic is used only for JWT role-based authorization demonstration purposes.
        public string Role { get; set; } = string.Empty;
    }
}
