using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

            //uncomment this line if you need to debug this code
            //then choose yes and create a new instance of visual
            //studio to step through the code
            //Debugger.Launch();

            //Seed Enums
            SeedEnum<DepartmentEnum>(typeof(Department), modelBuilder);
            
        }

        public void SeedEnum<T>(Type enumToParse, ModelBuilder mb) where T : class
        {
            List<T> enumObjectList = EnumToList<T>(enumToParse);

            foreach (var item in enumObjectList)
            {
                mb.Entity<T>().HasData(item);
            }
        }

        public List<T> EnumToList<T>(Type enumToParse) where T : class
        {
            Array enumValArray = Enum.GetValues(enumToParse);
            List<T> enumList = new List<T>();

            foreach (int val in enumValArray)
            {
                //create the object for the list
                T  item = (T)Activator.CreateInstance<T>();

                var id = val;
                var name = Enum.GetName(typeof(Department), val);
                var description = ((Department) val).GetDescription();

                TrySetProperty(item, "Id", id);
                TrySetProperty(item, "Name", name);
                TrySetProperty(item, "Description", description);
                TrySetProperty(item, "Deleted", false);

                enumList.Add(item);
            }

            return enumList;
        }

        private void TrySetProperty(object obj, string property, object value)
        {
            var prop = obj.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
                prop.SetValue(obj, value, null);
        }

    }
}
