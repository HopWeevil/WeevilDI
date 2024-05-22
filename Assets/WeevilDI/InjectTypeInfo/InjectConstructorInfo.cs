using System;
using System.Reflection;

namespace WeevilDI
{
    public sealed class InjectConstructorInfo
    {
        public readonly ConstructorInfo ConstructorInfo;
        public readonly Type[] ConstructorParameters;

        public InjectConstructorInfo(ConstructorInfo constructorInfo, Type[] constructorParameters)
        {
            ConstructorInfo = constructorInfo;
            ConstructorParameters = constructorParameters;
        }
    }
}