#nullable enable
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Redis.Extensions
{
    public static class EnumExtensions
    {
        public static string GetEnumDescription(this Enum myEnum)
        {
            if (myEnum == null)
                throw new ArgumentNullException(nameof(myEnum));

            var fi = myEnum.GetType().GetField(myEnum.ToString());

            if (fi?.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
                return attributes.First().Description;

            return myEnum.ToString();
        }
    }
}