using BackSystem.Application.Interfaces;
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
    public class AccountRepository: GenericRepository<AccountEntity>,IAccountRepository
    {
        public AccountRepository(BankSystemContext ctx):base(ctx)
        {
            
        }
    }
}
