using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgresser.Domain.Enums
{
    public static class EnumHelper
    {
        public static string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                 .FirstOrDefault() as DescriptionAttribute;

            return attribute?.Description ?? value.ToString();
        }
    }
}
