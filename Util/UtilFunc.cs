using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirList.Util
{
    public static class UtilFunc
    {
        public static string GetEnumDescription<T>(this T value) where T : Enum
        {
            var fileInfo = value.GetType().GetField(value.ToString());
            var descriptionAttribute = (DescriptionAttribute)fileInfo
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .FirstOrDefault();
            return descriptionAttribute.Description;
        }

        public static List<string> GetEnumDescriptionList<T>() where T : Enum
        {
            return typeof(T).GetEnumValues()
               .Cast<T>()
               .Select(value => value.GetEnumDescription())
               .ToList();
        }
        
    }
}
