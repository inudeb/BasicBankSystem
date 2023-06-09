using BankSystem.Domain;

namespace BankSystem.WebApp.Services.Interfaces
{
    public interface IAccountHttpService
    {
        Task<List<AccountEntity>>GetUserAccounts(int userID);
        Task<ServiceResult<AccountEntity>>CreateAccount(int userId, decimal balance);
        Task<ServiceResult<AccountEntity>> Withdraw(int accountId, decimal amount);
        Task<ServiceResult<AccountEntity>> Deposit(int accountId, decimal amount);
    }
}
