using JwtAuth.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuth.BLL.Interfaces
{
    public interface ITokenGenerationService
    {
        string GenerateJwt(User user);
    }
}
