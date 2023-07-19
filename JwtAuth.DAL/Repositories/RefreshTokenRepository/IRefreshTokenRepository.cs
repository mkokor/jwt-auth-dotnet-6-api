using JwtAuth.DAL.Entities;

namespace JwtAuth.DAL.Repositories.RefreshTokenRepository
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> CreateRefreshToken(RefreshToken refreshToken);

        Task<RefreshToken?> GetRefreshTokenByOwnerId(int ownerId);

        Task<RefreshToken?> GetRefreshTokenByValue(string value);
    }
}