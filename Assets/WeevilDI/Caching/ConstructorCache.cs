using System;
using System.Collections.Generic;

namespace WeevilDI
{
    internal class ConstructorCache
    {
        private static readonly Dictionary<Type, InjectConstructorInfo> _cache = new Dictionary<Type, InjectConstructorInfo>();
        
        internal static InjectConstructorInfo Get(Type type)
        {
            if (!_cache.TryGetValue(type, out var info))
            {            
                info = InjectableConstructorProvider.Provide(type);
                _cache.Add(type, info);
            }
            return info;
        }         
    }
}