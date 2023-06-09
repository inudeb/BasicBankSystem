using BackSystem.Application.Interfaces;
using BankSystem.Application.Interfaces;
using BankSystem.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _uow;

        public AccountService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<ServiceResult<AccountEntity>> CreateAccount(int userId, decimal balance)
        {
            if(balance < 100)
                return new ServiceResult<AccountEntity>().Error("The minimum amount required is $100.");
            if (balance > 10000)
                return new ServiceResult<AccountEntity>().Error("Cannot deposit more than $10,000 in a single transaction.");
            var newAccount = new AccountEntity { UserId = userId, Balance = balance };
            newAccount.Transactions.Add(new TransactionEntity() { Movement = balance, TransactionDate =  DateTime.Now });
            var added = await _uow.AccountRepository.Add(newAccount);
            _uow.SaveChanges();
            return new ServiceResult<AccountEntity>().Ok(added, "Account created successfully.");
        }

        public async Task<ServiceResult<AccountEntity>> Deposit(int accountId, decimal amount)
        {
            //get the account
            var account = _uow.AccountRepository.GetQueryable().Include(c => c.Transactions)
                .SingleOrDefault(x => x.AccountId == accountId);
            if(account==null)
                return new ServiceResult<AccountEntity>().Error("Account not found");
            //validate if we can withdraw the amount
            if(amount<10000)
            {
                account.Balance += amount;
                account.Transactions.Add(new TransactionEntity { Movement = amount, TransactionDate = DateTime.Now });
                _uow.SaveChanges();
            }
            else
                return new ServiceResult<AccountEntity>().Error(account, "Cannot deposit more than $10,000 in a single transaction.");
            return new ServiceResult<AccountEntity>().Ok(account, "Transaction completed successfully.");
        }

        public async Task<List<AccountEntity>> GetAccountByUser(int userId)
        {
            return await _uow.AccountRepository.GetQueryable().Where(c => c.UserId == userId).Include(c=> c.Transactions)
                .ToListAsync();
        }

        public async Task<ServiceResult<AccountEntity>> Withdraw(int accountId, decimal amount)
        {
            //get the account
            var account =  _uow.AccountRepository.GetQueryable().Include(c => c.Transactions)
                .SingleOrDefault(x => x.AccountId == accountId);
            if (account == null)
                return new ServiceResult<AccountEntity>().Error("Account not found");
            if ((amount / account.Balance) <= 0.9m)
            {
                if (((account.Balance - amount) > 100))
                {
                    account.Balance -= amount;
                    account.Transactions.Add(new TransactionEntity { Movement = -amount, TransactionDate = DateTime.Now });
                    _uow.SaveChanges();
                }
                else
                    return new ServiceResult<AccountEntity>().Error(account, "The account can't have less than $100.");
            }
            else
                return new ServiceResult<AccountEntity>().Error(account, "Cannot withdraw more than 90% of the total balance on a single transacction.");
            return new ServiceResult<AccountEntity>().Ok(account, "Transaction completed successfully.");
        }
    }
}
