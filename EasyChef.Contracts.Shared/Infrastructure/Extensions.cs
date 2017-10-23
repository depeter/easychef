using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace EasyChef.Shared.Infrastructure
{
    public static class Extensions
    {
        public static string ReportAllProperties<T>(this T instance) where T : class
        {
            if (instance == null)
                return string.Empty;

            var strListType = typeof(List<string>);
            var strArrType = typeof(string[]);

            var arrayTypes = new[] { strListType, strArrType };
            //var handledTypes = new[] { typeof(bool), typeof(Int32), typeof(Int64), typeof(String), typeof(DateTime), typeof(double), typeof(decimal), strListType, strArrType };

            var validProperties = instance.GetType()
                                          .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                          //.Where(prop => handledTypes.Contains(prop.PropertyType))
                                          .Where(prop => prop.GetValue(instance, null) != null)
                                          .ToList();

            if (validProperties.Count == 0) return "No properties found.";

            var format = string.Format("\t{{0,-{0}}} : {{1}}", validProperties.Max(prp => prp.Name.Length));

            return string.Join(
                     Environment.NewLine,
                     validProperties.Select(prop => string.Format(format,
                                                                  prop.Name,
                                                                  (arrayTypes.Contains(prop.PropertyType) ? string.Join(", ", (IEnumerable<string>)prop.GetValue(instance, null))
                                                                                                          : prop.GetValue(instance, null)))));
        }
    }
}
