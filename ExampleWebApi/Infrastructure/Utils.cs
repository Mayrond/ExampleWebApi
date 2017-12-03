using System;
using System.Collections.Generic;
using System.Linq;

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
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var assemblyTypes = assembly.GetTypes();
            return assemblyTypes.Where(t => t.BaseType == baseType).ToList();
        }
    }
}