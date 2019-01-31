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

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<DepartmentEnum> Departments { get; set; }


        protected internal virtual void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (Type enumType in Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.IsSubclassOf(typeof(Enum)) &&
                            x.GetCustomAttribute<DescriptionAttribute>() != null))
            {

                var tablename = enumType.Name.Replace("Enum", String.Empty);

                var enumObject = Activator.CreateInstance(null, enumType);
                var enumDbObject = Activator.CreateInstance(null, tablename);
                
                Array names = Enum.GetNames(enumType);
                Array ids = Enum.GetValues(enumType);


                for (int i = 0; i < names.Length; i++)
                {
                    string val = (string)names.GetValue(i);
                    int id = (int)ids.GetValue(i);
                    
                    enumDbObject = new { Id = id, Name = val };

                    modelBuilder.Entity(tablename).HasData(enumDbObject);

                    //PropertyInfo nameProp = enumType.GetProperty("Name");
                    //if (nameProp != null)
                    //    nameProp.SetValue(typeof(string), val);

                    
                    //PropertyInfo idProp = enumType.GetProperty("Id");
                    //if (idProp != null)
                    //    idProp.SetValue(typeof(int), val);

                    //string description = StringValueOfEnum(val);

                    //PropertyInfo descriptionProp = enumType.GetProperty("Description");
                    //    var description = prop.GetCustomAttribute<DescriptionAttribute>();

                    //    PropertyInfo prop = enumType.GetProperty("Name");
                    //}
                
                    
                }

            }
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options
        //        .ReplaceService<IMigrationsSqlGenerator, CustomMigrationsSqlGenerator>();



    }
}
