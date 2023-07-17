using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuth.BLL.DTOs.Responses
{
    public class JwtRefreshResponseDto
    {
        public string JsonWebToken { get; set; } = string.Empty;
    }
}
