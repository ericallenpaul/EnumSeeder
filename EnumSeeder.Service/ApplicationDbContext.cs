using System;
using System.Collections.Generic;
using System.Text;
using EnumSeeder.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EnumSeeder.Service
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<DepartmentEnum> Departments { get; set; }


        protected internal virtual void OnModelCreating(ModelBuilder modelBuilder)
        {

        }


    }
}
