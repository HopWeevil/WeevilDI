using System.Reflection;

namespace WeevilDI
{
    public class InjectableAttributesInfo
    {
        public readonly FieldInfo[] InjectableFields;
        public readonly PropertyInfo[] InjectableProperties;
        public readonly InjectMethodInfo[] InjectableMethods;

        public InjectableAttributesInfo(FieldInfo[] injectableFields, PropertyInfo[] injectableProperties, InjectMethodInfo[] injectableMethods)
        {
            InjectableFields = injectableFields;
            InjectableProperties = injectableProperties;
            InjectableMethods = injectableMethods;
        }

    }
}