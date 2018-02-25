using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DNX.Helpers.Converters;

namespace DNX.CommandLineParser.Helpers
{
    public static class PropertyHelper
    {
        public static IList<Type> KnownNonEnumerableTypes = new List<Type>
        {
            typeof(string)
        };

        public static Type GetPropertyType(PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType;
        }

        public static Type GetUnderlyingPropertyType(PropertyInfo propertyInfo)
        {
            var propertyType = GetPropertyType(propertyInfo);

            if (!IsEnumerable(propertyInfo))
                return propertyType;

            var underlyingType = typeof(object);
            if (propertyType.GetInterfaces().Any())
            {
                var mainInterface = propertyType.GetInterfaces().First();
                if (mainInterface != null)
                {
                    underlyingType = mainInterface.GenericTypeArguments.First();
                }
            }

            return underlyingType;
        }

        public static Type GetEnumerableType(Type type)
        {
            if (!IsEnumerableType(type))
                return null;

            if (type.BaseType == typeof(Array))
                return type.BaseType;

            if (!type.GetInterfaces().Any())
                return null;

            var mainInterface = type.GetInterfaces().First();
            if (mainInterface == null)
                return null;

            var underlyingType = mainInterface.UnderlyingSystemType;

            return underlyingType;
        }

        public static bool IsEnumerable(PropertyInfo propertyInfo)
        {
            var propertyType = GetPropertyType(propertyInfo);

            return IsEnumerableType(propertyType);
        }

        public static bool IsEnumerableType(Type type)
        {
            return !KnownNonEnumerableTypes.Contains(type)
                   && type.GetInterfaces().ToList().Contains(typeof(IEnumerable));
        }

        private static int GetEnumerableCount(PropertyInfo propertyInfo, object instance)
        {
            if (!IsEnumerable(propertyInfo))
                return 0;

            var enumerable = propertyInfo.GetValue(instance) as IEnumerable;
            return enumerable == null
                ? 0
                : enumerable.Cast<object>().Count();
        }

        public static void SetValue(PropertyInfo propertyInfo, object instance, object value)
        {
            var propertyType           = GetPropertyType(propertyInfo);
            var underlyingPropertyType = GetUnderlyingPropertyType(propertyInfo);

            if (underlyingPropertyType.IsEnum)
            {
                SetEnumValue(propertyInfo, instance, value);

                return;
            }

            var convertedValue = value.ChangeType(underlyingPropertyType);

            if (IsEnumerable(propertyInfo))
            {
                SetEnumerableValue(propertyInfo, instance, convertedValue);

                return;
            }

            propertyInfo.SetValue(instance, convertedValue);
        }

        private static void SetEnumValue(PropertyInfo propertyInfo, object instance, object value)
        {
            var propertyType = GetPropertyType(propertyInfo);

            var stringValue = Convert.ToString(value);

            long longValue;
            var enumValue = long.TryParse(stringValue, out longValue)
                ? Enum.ToObject(propertyType, longValue)
                : Enum.Parse(propertyType, stringValue);

            propertyInfo.SetValue(instance, enumValue);
        }

        private static void SetEnumerableValue(PropertyInfo propertyInfo, object instance, object convertedValue)
        {
            var propertyType = GetPropertyType(propertyInfo);

            var enumerableType = GetEnumerableType(propertyType);
            if (enumerableType == typeof(List<>))
            {
                SetListValue(propertyInfo, instance, convertedValue);
            }
            else
            {
                SetArrayValue(propertyInfo, instance, convertedValue);
            }
        }

        public static void SetListValue(PropertyInfo propertyInfo, object instance, object convertedValue)
        {
            var currentValue = propertyInfo.GetValue(instance);
            if (currentValue == null)
            {
                // TODO: Create List of Underlyin Type
                currentValue = Activator.CreateInstance(propertyInfo.PropertyType);
                propertyInfo.SetValue(instance, null);
            }

            var list = currentValue as IList;
            if (list == null)
            {
                return;
            }

            list.Add(convertedValue);
        }

        public static void SetArrayValue(PropertyInfo propertyInfo, object instance, object convertedValue)
        {
            var propertyType = GetPropertyType(propertyInfo);
            var currentValue = propertyInfo.GetValue(instance);

            var array = currentValue as Array;
            if (array == null)
                return;

            var list = new List<object>();
            foreach(var a in array)
                list.Add(a);

            list.Add(convertedValue);

            array = list.ToArray();

            propertyInfo.SetValue(instance, array);
        }
    }
}
