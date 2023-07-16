using JwtAuth.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuth.BLL.Utilities.TokenGenerationService
{
    public interface ITokenGenerationService
    {
        string GenerateJwt(User user);
    }
}
