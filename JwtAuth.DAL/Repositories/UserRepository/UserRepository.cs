using JwtAuth.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuth.DAL.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _appDbContext.Users.ToListAsync();
        }

        public async Task CreateUser(User user)
        {
            await _appDbContext.Users.AddAsync(user);
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(user => user.Username.Equals(username));
        }
    }
}