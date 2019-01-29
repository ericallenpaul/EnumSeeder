using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using EnumSeeder.Models;
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

            //get the dbContext
            var context = new ApplicationDbContext(builder.Options);

            //add in our enum
            Debugger.Launch();
            EnumHelper.SeedEnumData<DepartmentEnum, Department>(context.Departments, context);

            return context;
        }
    }

}
