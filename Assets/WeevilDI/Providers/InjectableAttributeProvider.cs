using System;
using System.Linq;
using System.Reflection;
using WeevilDI.Attributes;

namespace WeevilDI
{
    public class InjectableAttributeProvider
    {
        private const BindingFlags Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;

        private static FieldInfo[] GetInjectableFields(Type type)
        {
            return type.GetFields(Flags).Where(f => f.IsDefined(typeof(InjectAttribute))).ToArray();
        }

        private static PropertyInfo[] GetInjectableProperties(Type type)
        {
            return type.GetProperties(Flags).Where(p => p.CanWrite && p.IsDefined(typeof(InjectAttribute))).ToArray();
        }

        private static InjectMethodInfo[] GetInjectableMethods(Type type)
        {
            return type.GetMethods(Flags).Where(m => m.IsDefined(typeof(InjectAttribute))).Select(m => new InjectMethodInfo(m, m.GetParameters())).ToArray();
        }

        public static InjectableAttributesInfo Provide(Type type)
        {
            return new InjectableAttributesInfo(GetInjectableFields(type), GetInjectableProperties(type), GetInjectableMethods(type));
        }
    }
}