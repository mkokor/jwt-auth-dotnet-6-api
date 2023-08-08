using JwtAuth.DAL.Entities;

namespace JwtAuth.BLL.Utilities.TokenGenerationService
{
    public interface ITokenGenerationService
    {
        string GenerateAccessToken(User user);

        string GenerateRefreshToken();
    }
}