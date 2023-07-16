using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JwtAuth.DAL.Repositories.UserRepository;

namespace JwtAuth.DAL.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        Task SaveAsync();
    }
}
