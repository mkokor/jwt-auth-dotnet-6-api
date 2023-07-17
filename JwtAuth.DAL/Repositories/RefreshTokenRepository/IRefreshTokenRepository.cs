using JwtAuth.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuth.DAL.Repositories.RefreshTokenRepository
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> CreateRefreshToken(RefreshToken refreshToken);

        Task<RefreshToken?> GetRefreshTokenByOwnerId(int ownerId);
    }
}
