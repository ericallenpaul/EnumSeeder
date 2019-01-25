using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EnumSeeder.Models;
using EnumSeeder.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnumSeeder.API
{
    public static class ServiceExtensions
    {

        public static IServiceCollection RegisterMyServices(this IServiceCollection services, IConfiguration config)
        {
            try
            {
                // Add other services
                services.AddTransient<IEmployeeService, EmployeeService>();
                return services;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
