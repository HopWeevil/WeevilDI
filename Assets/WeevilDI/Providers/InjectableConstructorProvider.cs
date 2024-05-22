using System;
using System.Linq;
using System.Reflection;
using WeevilDI.Attributes;

namespace WeevilDI
{
    public class InjectableConstructorProvider
    {
        public static InjectConstructorInfo Provide(Type type)
        {          
            ConstructorInfo[] constructors = type.GetConstructors().OrderByDescending(c => c.GetParameters().Length).ToArray();
            ConstructorInfo constructor = constructors.FirstOrDefault();

            if (constructor == null)
            {
                throw new Exception($"No injectable constructor found for type: {type}");
            }

           // ParameterInfo[] parameters = constructor.GetParameters().ToArray();
            var parameters = constructor.GetParameters().Select(p => p.ParameterType).ToArray();
            return new InjectConstructorInfo(constructor, parameters);
           
        }

    }
}