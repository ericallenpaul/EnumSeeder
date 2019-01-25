using System;
using EnumSeeder.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EnumSeeder.Service
{
    public interface IEmployeeService
    {
        Employee Add(Employee employee);
        bool Delete(Employee employee);
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _Context;
        private readonly EFRepository<ApplicationDbContext> _Repository;
        private readonly EnumSeeder_Settings _Settings;
        private readonly IMemoryCache _Cache;
        private ILogger<EmployeeService> _Logger;

        public EmployeeService(
            ApplicationDbContext Context,
            IOptions<EnumSeeder_Settings> Settings,
            IMemoryCache MemoryCache,
            ILogger<EmployeeService> Logger,
            EFRepository<ApplicationDbContext> Repository
        )
        {
            _Context = Context;
            _Settings = Settings.Value;
            _Cache = MemoryCache ?? new MemoryCache(new MemoryCacheOptions());
            _Logger = Logger;
            _Repository = Repository;
        }


        public Employee Add(Employee employee)
        {
            Employee returnValue = null;

            try
            {
                _Repository.Create(employee);
                _Repository.Save();

                if (employee.Id > 0)
                {
                    returnValue = employee;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return returnValue;
        }

        public bool Delete(Employee employee)
        {
            bool returnValue = false;

            try
            {
                _Repository.Delete(employee);
                _Repository.Save();
                returnValue = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return returnValue;
        }

    }
}
