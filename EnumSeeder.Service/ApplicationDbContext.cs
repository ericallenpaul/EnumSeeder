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
            //only call this if you're using IdentityDbContext
            //regular DbContext shouldn't need this call
            base.OnModelCreating(modelBuilder);

            //uncomment this line if you need to debug this code
            //then choose yes and create a new instance of visual
            //studio to step through the code. The debugger launches
            //wherever this line is created
            //Debugger.Launch();

            //Seed Enums
            SeedEnum<DepartmentEnum,Department>(modelBuilder);
        }

        public void SeedEnum<T,TEnum>(ModelBuilder mb) where T : class
            where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            List<T> enumObjectList = EnumToList<T, TEnum>();

            foreach (var item in enumObjectList)
            {
                mb.Entity<T>().HasData(item);
            }
        }

        public List<T> EnumToList<T,TEnum>() where T : class 
            where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            //get an array fo all of the values of the enum
            Array enumValArray = Enum.GetValues(typeof(TEnum));

            //Create the Enum Object list
            List<T> enumList = new List<T>();

            //loop through the enum values
            foreach (int val in enumValArray)
            {
                //create the object for the list
                T  item = (T)Activator.CreateInstance<T>();

                //get the values from the enum
                var id = val;
                var name = Enum.GetName(typeof(TEnum), val);
                var description = GetEnumDescription<TEnum>(name);
             
                //set values to the properties of our Generic Enum Class
                TrySetProperty(item, "Id", id);
                TrySetProperty(item, "Name", name);
                TrySetProperty(item, "Description", description);
                TrySetProperty(item, "Deleted", false);

                //add the item to the list
                enumList.Add(item);
            }

            //return the list
            return enumList;
        }

        private void TrySetProperty(object obj, string property, object value)
        {
            var prop = obj.GetType().GetProperty(property, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
                prop.SetValue(obj, value, null);
        }

        public static KeyValuePair<string, List<EnumDescription>> ConvertEnumWithDescription<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new Exception("Type given T must be an Enum");
            }

            var enumType = typeof(T).ToString().Split('.').Last();
            var type = typeof(T);

            var itemsList = Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(x => new EnumDescription
                {
                    Key = Convert.ToInt32(x),
                    Value = GetEnumDescription<T>(Enum.GetName(typeof(T), x))
                })
                .ToList();

            var res = new KeyValuePair<string, List<EnumDescription>>(
                enumType, itemsList);
            return res;

        }

        public static string GetEnumDescription<T>(string enumValue)
        {
            var value = Enum.Parse(typeof(T), enumValue);
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            return value.ToString();
        }

    }

    
}
