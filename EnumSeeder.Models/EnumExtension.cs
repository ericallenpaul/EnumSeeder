using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EnumSeeder.Models
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum GenericEnum) 
        {
            Type genericEnumType = GenericEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());
            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if (attributes != null && attributes.Any())
                {
                    return ((System.ComponentModel.DescriptionAttribute)attributes.ElementAt(0)).Description;
                }
            }
            return GenericEnum.ToString();
        }

        public static T ToEnum<T>(this string enumString)
        {
            return (T)Enum.Parse(typeof(T), enumString);
        }
    }
}
