using JwtAuth.DAL.Entities;

namespace JwtAuth.BLL.Utilities.TokenGenerationService
{
    public interface ITokenGenerationService
    {
        string GenerateAccessToken(User user);

        RefreshToken GenerateRefreshToken(User user);
    }
}