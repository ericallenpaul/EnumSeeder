using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EnumSeeder.Service
{
    public class DbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;Database=EnumSeeder;Trusted_Connection=True;MultipleActiveResultSets=true");

            var returnValue = new ApplicationDbContext(builder.Options);

            return returnValue;
        }
    }

}
