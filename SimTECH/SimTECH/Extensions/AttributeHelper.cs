using System.ComponentModel;
using System.Reflection;

namespace SimTECH.Extensions
{
    public static class AttributeHelper
    {
        public static string GetDescription<T>(this T source)
        {
            FieldInfo field = source.GetType().GetField(source.ToString());

            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;

            return source.ToString();
        }
    }
}
