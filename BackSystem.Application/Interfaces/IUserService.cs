using BankSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResult<UserEntity>> AuthenticateUser(string userName, string pass);

        Task<ServiceResult<UserEntity>> GetUserData(int userId);
    }
}
