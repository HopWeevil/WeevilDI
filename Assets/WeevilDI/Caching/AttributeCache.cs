using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace WeevilDI
{
    internal class AttributeCache
    {
        private static readonly Dictionary<Type, InjectableAttributesInfo> _cache = new Dictionary<Type, InjectableAttributesInfo>();
        
        internal static InjectableAttributesInfo Get(Type type)
        {
            if (!_cache.TryGetValue(type, out var info))
            {            
                info = InjectableAttributeProvider.Provide(type);
                _cache.Add(type, info);
            }
            return info;
        }

     
    }
}