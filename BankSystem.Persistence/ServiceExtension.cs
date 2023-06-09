
using BackSystem.Application.Interfaces;
using BackSystem.Application.Interfaces.Repositories;
using BankSystem.Persistence.Contexts;
using BankSystem.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Persistence
{
    public static class ServiceExtension
    {
        public static void RegisterPersistence(this IServiceCollection services)
        {
            services.AddDbContext<BankSystemContext>(options =>
                     options.UseInMemoryDatabase("BasicBankDb"));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            
        }
    }
}
