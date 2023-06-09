using BackSystem.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackSystem.Application.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IAccountRepository AccountRepository { get; }
        IUserRepository UserRepository { get; }
        int SaveChanges();
    }
}
