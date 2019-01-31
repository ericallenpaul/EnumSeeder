using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using EnumSeeder.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EnumSeeder.Service
{
    public class DbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            //uncomment this line if you need to debug this code
            //then choose yes and create a new instance of visual
            //studio to step through the code
            Debugger.Launch();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder
                .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=EnumSeeder;Trusted_Connection=True;MultipleActiveResultSets=true");

            

            //get the dbContext
            var context = new ApplicationDbContext(builder.Options);

            

            return context;
        }
    }

}
