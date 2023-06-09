using BankSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Interfaces
{
    public interface IAccountService
    {
        Task<ServiceResult<AccountEntity>> CreateAccount(int userId, decimal balance);

        Task<ServiceResult<AccountEntity>> Withdraw(int accountId, decimal amount);

        Task<ServiceResult<AccountEntity>> Deposit(int accountId, decimal amount);

        Task<List<AccountEntity>> GetAccountByUser(int userId);
    }
}
