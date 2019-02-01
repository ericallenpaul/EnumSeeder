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

            //Debugger.Launch();

            SeedEnum<DepartmentEnum>(typeof(Department), modelBuilder);

            //var query = Assembly.GetExecutingAssembly()
            //    .GetTypes()
            //    .Where(t => t.IsEnum && t.Namespace == "EnumSeeder.Models");

            //foreach (Type t in query)
            //{
            //    Console.WriteLine(t.FullName);
            //}

            //foreach (var assemblyName in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
            //{
            //    Assembly assembly = Assembly.Load(assemblyName);
            //    foreach (var type in assembly.GetTypes())
            //    {
            //        Console.WriteLine(type.Name);
            //    }
            //}



            //modelBuilder.Entity<DepartmentEnum>().HasData(
            //    new DepartmentEnum() { Id = 1, Name = "Sales", Description = "Sales", Deleted = false },
            //    new DepartmentEnum() { Id = 2, Name = "Customer Service", Description = "Customer Service", Deleted = false },
            //    new DepartmentEnum() { Id = 3, Name = "TechnicalSupport", Description = "Technical Support", Deleted = false }
            //);
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
