using JwtAuth.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuth.DAL.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();

        Task CreateUser(User user);

        Task<User?> GetUserByUsername(string username);
    }
}
