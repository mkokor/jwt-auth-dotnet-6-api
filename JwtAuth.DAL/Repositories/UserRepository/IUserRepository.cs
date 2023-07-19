using JwtAuth.DAL.Entities;

namespace JwtAuth.DAL.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();

        Task CreateUser(User user);

        Task<User?> GetUserByUsername(string username);

        Task<User?> GetUserById(int userId);
    }
}