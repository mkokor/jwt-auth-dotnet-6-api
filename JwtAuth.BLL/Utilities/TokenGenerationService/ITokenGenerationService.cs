using JwtAuth.DAL.Entities;

namespace JwtAuth.BLL.Utilities.TokenGenerationService
{
    public interface ITokenGenerationService
    {
        string GenerateJwt(User user);

        RefreshToken GenerateRefreshToken(int ownerId);
    }
}