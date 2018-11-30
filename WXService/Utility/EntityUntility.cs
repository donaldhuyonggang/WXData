using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WXService.Utility
{
    public static class EntityUntility
    {
        public static void CopyProperty<T>(T source, T target)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            Func<PropertyInfo, bool> filter = p => p.CanRead && p.CanWrite && !p.GetMethod.IsVirtual ;
           
            var sourceProperties = typeof(T).GetProperties(flags).Where(filter);
            var targetProperties = typeof(T).GetProperties(flags).Where(filter);

            foreach (var property in targetProperties)
            { 
                var s = sourceProperties.SingleOrDefault(p => p.Name.Equals(property.Name)
                        && property.DeclaringType.IsAssignableFrom(p.DeclaringType));
                if (s != null)
                {

                    property.SetValue(target, s.GetValue(source, null), null);
                }
                 
            }
        }
    }
}
