using BankSystem.Domain;

namespace BankSystem.WebApp.Services.Interfaces
{
    public interface IUserHttpService
    {
        public Task<ServiceResult<UserEntity>> AuthenticateUser(string user,string password);

        public Task<ServiceResult<UserEntity>> GetUserData(int userId);
    }
}
