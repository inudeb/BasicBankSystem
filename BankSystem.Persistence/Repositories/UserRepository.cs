using BackSystem.Application.Interfaces.Repositories;
using BankSystem.Domain;
using BankSystem.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Persistence.Repositories
{
    public class UserRepository:GenericRepository<UserEntity>, IUserRepository
    {
        public UserRepository(BankSystemContext ctx) : base(ctx)
        {
            
        }
    }
}
