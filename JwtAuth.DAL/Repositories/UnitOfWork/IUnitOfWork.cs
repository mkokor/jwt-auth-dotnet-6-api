using JwtAuth.DAL.Repositories.RefreshTokenRepository;
using JwtAuth.DAL.Repositories.UserRepository;

namespace JwtAuth.DAL.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; }

        Task SaveAsync();
    }
}