using System.Reflection;

namespace WeevilDI
{
    public sealed class InjectMethodInfo
    {
        public readonly MethodInfo MethodInfo;
        public readonly ParameterInfo[] Parameters;

        public InjectMethodInfo(MethodInfo methodInfo, ParameterInfo[] parameters)
        {
            MethodInfo = methodInfo;
            Parameters = parameters;
        }
    }
}