using System;
using System.ComponentModel;
using System.Reflection;

namespace OlxAPI.Models
{
    public enum RolesEnum
    {
        [Description("Admin")]
        Admin = 1,
        [Description("User")]
        User = 2,
    }
    public static class EnumExtensions
    {
        public static string GetEnumDescription(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            return fi.GetCustomAttribute<DescriptionAttribute>().Description;
        }
    }
}