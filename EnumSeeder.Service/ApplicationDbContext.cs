using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using EnumSeeder.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EnumSeeder.Service
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        public DbSet<Employee> Employees { get; set; }

        public DbSet<DepartmentEnum> Departments { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DepartmentEnum>().HasData(
                new DepartmentEnum() { Id = 1, Name = "Sales", Description = "Sales", Deleted = false },
                new DepartmentEnum() { Id = 2, Name = "Customer Service", Description = "Customer Service", Deleted = false },
                new DepartmentEnum() { Id = 3, Name = "TechnicalSupport", Description = "Technical Support", Deleted = false }
            );
        }
    }
}
