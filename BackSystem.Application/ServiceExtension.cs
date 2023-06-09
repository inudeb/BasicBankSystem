using BankSystem.Application.Interfaces;
using BankSystem.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application
{
    public static class ServiceExtension
    {
        public static void RegisterApplication(this IServiceCollection services) 
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService,AccountService>();
        }
    }
}
