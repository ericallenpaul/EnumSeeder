using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using EnumSeeder.Models;
using Microsoft.EntityFrameworkCore;

namespace EnumSeeder.Service
{
    public class EnumHelper
    {
        public static void SeedEnumData<TData, TEnum>(DbSet<TData> items, ApplicationDbContext context)
            where TData : EnumBase<TEnum>
            where TEnum : struct
        {
            List<int> existingItemIds = new List<int>();

            //get a list of existing IDs
            //if the database has been created
            try
            {
                existingItemIds = context.Set<TData>().Select(x => x.Id).ToList();
            }
            catch (Exception e)
            {
                var enumItem1 = Activator.CreateInstance<TData>();
                var enumName = enumItem1.ToString();
                System.Console.WriteLine($"Cannot find the database while trying to check the enum: {enumName}. \r\nThis may not be a problem if this is the initial creation");
                return;
            }

            var primaryEnumType = typeof(TEnum);

            if (!primaryEnumType.IsEnum)
                throw new Exception(string.Format("The type '{0}' must be of type enum", primaryEnumType.AssemblyQualifiedName));

            var enumType = Enum.GetUnderlyingType(primaryEnumType);

            if (enumType == typeof(long) || enumType == typeof(ulong) || enumType == typeof(uint))
                throw new Exception();

            foreach (TEnum enumValue in Enum.GetValues(primaryEnumType))
            {
                var enumItem = Activator.CreateInstance<TData>();

                enumItem.Id = (int)Convert.ChangeType(enumValue, typeof(int));

                if (enumItem.Id < 1)
                    throw new Exception("Enum value must be positive number greater than zero. You may need to set an explicit enum value");

                enumItem.Name = Enum.GetName(primaryEnumType, enumValue);

                enumItem.Description = GetEnumDescription(enumValue);
                
                if (!existingItemIds.Contains(enumItem.Id))
                {
                    items.Add(enumItem);
                }
            }
        }

        public static string GetEnumDescription<TEnum>(TEnum enumItem)
        {
            Type type = enumItem.GetType();

            var attribute = type.GetField(enumItem.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false).Cast<DescriptionAttribute>().FirstOrDefault();
            return attribute == null ? string.Empty : attribute.Description;
        }
    }
}
