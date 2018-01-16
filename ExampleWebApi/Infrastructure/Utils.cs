using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace ExampleWebApi.Infrastructure
{
    public static class Utils
    {
        public static List<Type> GetTypeByName(string className)
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var assemblyTypes = assembly.GetTypes();
            return assemblyTypes.Where(t => t.Name == className).ToList();
        }

        public static List<Type> GetTypeByParent(Type baseType)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyTypes = assembly.GetTypes();
            return assemblyTypes.Where(t => t.BaseType == baseType).ToList();
        }

        public static List<PropertyInfo> GetDbSetProperties(this System.Data.Entity.DbContext context)
        {
            var dbSetProperties = new List<PropertyInfo>();
            var properties = context.GetType().GetProperties();

            foreach (var property in properties)
            {
                var setType = property.PropertyType;

            var isDbSet = setType.IsGenericType && (typeof (IDbSet<>).IsAssignableFrom(setType.GetGenericTypeDefinition()) || setType.GetInterface(typeof (IDbSet<>).FullName) != null);


                if (isDbSet)
                {
                    dbSetProperties.Add(property);
                }
            }

            return dbSetProperties;

        }
    }
}