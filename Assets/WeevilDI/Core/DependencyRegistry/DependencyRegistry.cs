using System;
using System.Collections.Generic;

namespace WeevilDI.Core
{
    public class DependencyRegistry
    {
        private readonly Dictionary<Type, Dependency> _dependencies = new Dictionary<Type, Dependency>();

        public bool TryGetDependency(Type type, out object dependency)
        {
            if (_dependencies.TryGetValue(type, out var dep))
            {
                dependency = dep.Instance;
                return true;
            }
            dependency = null;
            return false;
        }

        public void Remove(Type type)
        {
            _dependencies.Remove(type);
        }
        public void Add<T>(T dependency, Lifetime lifetime)
        {
            _dependencies[typeof(T)] = new Dependency(dependency, lifetime);
        }

        public void RemoveDependencies(Lifetime lifetime)
        {
            var dependenciesToRemove = new List<Type>();

            foreach (var kvp in _dependencies)
            {
                if (kvp.Value.Lifetime == lifetime)
                {
                    dependenciesToRemove.Add(kvp.Key);
                }
            }

            foreach (var key in dependenciesToRemove)
            {
                _dependencies.Remove(key);
            }
        }
    }
}