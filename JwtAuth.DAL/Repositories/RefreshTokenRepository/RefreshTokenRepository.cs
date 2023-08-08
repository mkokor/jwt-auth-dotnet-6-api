using JwtAuth.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace JwtAuth.DAL.Repositories.RefreshTokenRepository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _appDbContext;

        public RefreshTokenRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<RefreshToken?> CreateRefreshToken(RefreshToken refreshToken)
        {
            await _appDbContext.RefreshTokens.AddAsync(refreshToken);
            return refreshToken;
        }

        public async Task<RefreshToken?> GetRefreshTokenByOwnerId(int ownerId)
        {
            return await _appDbContext.RefreshTokens.Include(refreshToken => refreshToken.Owner)
                                                    .FirstOrDefaultAsync(refreshToken => refreshToken.OwnerId == ownerId);
        }

        public async Task<List<RefreshToken>> GetAllRefreshTokens()
        {
            return await _appDbContext.RefreshTokens.Include(refreshToken => refreshToken.Owner)
                                                    .ToListAsync();
        }

        public void DeleteRefreshToken(RefreshToken refreshToken)
        {
            _appDbContext.RefreshTokens.Remove(refreshToken);
        }
    }
}