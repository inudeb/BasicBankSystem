using BackSystem.Application.Interfaces;
using BankSystem.Application.Interfaces;
using BankSystem.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;


        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

       
        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public  async Task<ServiceResult<UserEntity>> AuthenticateUser(string userName, string pass)
        {
            var user = await _uow.UserRepository.GetQueryable().Where(c => c.UserName == userName).FirstOrDefaultAsync();
            var users = await _uow.UserRepository.GetAll();
            //we create the user
            if (user == null)
            {
                var salt = RandomNumberGenerator.GetBytes(keySize);
                var newUser = new UserEntity()
                {
                    UserName = userName,
                    UserPassword = HashPasword(pass, salt),
                    Salt = Convert.ToHexString(salt)
                };
                await _uow.UserRepository.Add(newUser);
                _uow.SaveChanges();
                return new ServiceResult<UserEntity>().Ok(newUser, "User created");
            }
            else//validate password
            {
                if(VerifyPassword(pass, user.UserPassword, Convert.FromHexString(user.Salt))) 
                {
                    return new ServiceResult<UserEntity>().Ok(user,"Login Successfull");
                }
                else
                    return new ServiceResult<UserEntity>().Error("Incorrect password or user name");
            }
        }

        public async Task<ServiceResult<UserEntity>> GetUserData(int userId)
        {
            var data = await _uow.UserRepository.GetQueryable().SingleOrDefaultAsync(c => c.UserId == userId);
            if(data == null)
                return new ServiceResult<UserEntity>().Error("User not found");
            return new ServiceResult<UserEntity>().Ok(data,string.Empty);
        }

        private string HashPasword(string password, byte[] salt)
        {
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToHexString(hash);
        }

        private bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
    }
}
