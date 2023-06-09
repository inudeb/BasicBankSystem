using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackSystem.Application.Interfaces;
using BackSystem.Application.Interfaces.Repositories;
using BankSystem.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BankSystemContext _dbContext;

        public IAccountRepository AccountRepository { get; }

        public IUserRepository UserRepository { get; }

        public UnitOfWork(BankSystemContext dbContext, IAccountRepository accountRepository, IUserRepository userRepository)
        {
            _dbContext = dbContext;
            AccountRepository = accountRepository;
            UserRepository = userRepository;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
