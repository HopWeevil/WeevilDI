using System;
using System.Reflection;
using WeevilDI.Configuration;
using WeevilDI.Core;

namespace WeevilDI.Injectors
{
    public class ReflectionInjector
    {
        private readonly DIContainer _container;
        private readonly DIConfiguration _configuration;

        public ReflectionInjector(DIContainer container, DIConfiguration configuration)
        {
            _container = container;
            _configuration = configuration;
        }

        public object CreateInstance(Type type)
        {
            InjectConstructorInfo info = GetInjectConstructor(type);

            var arguments = TypedArrayPool<object>.Shared.Rent(info.ConstructorParameters.Length);
            try
            {
                for (var i = 0; i < info.ConstructorParameters.Length; i++)
                {
                    arguments[i] = _container.Resolve(info.ConstructorParameters[i]);
                }
                var instance = info.ConstructorInfo.Invoke(arguments);
                InjectAttributes(instance);
                return instance;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to resolve {info.ConstructorInfo} : {ex.Message}");
            }
            finally
            {
                TypedArrayPool<object>.Shared.Return(arguments);
            }
        }

        private InjectConstructorInfo GetInjectConstructor(Type type)
        {
            if (_configuration.CacheConstructors)
            {
                return ConstructorCache.Get(type);
            }
            else
            {
                return InjectableConstructorProvider.Provide(type);
            }
        }

        public void InjectAttributes(object target)
        {
            InjectableAttributesInfo info = GetInjectableAttributes(target);

            for (int i = 0; i < info.InjectableFields.Length; i++)
            {
                InjectField(info.InjectableFields[i], target);
            }
            for (int i = 0; i < info.InjectableProperties.Length; i++)
            {
                InjectProperty(info.InjectableProperties[i], target);
            }
            for (int i = 0; i < info.InjectableMethods.Length; i++)
            {
                InjectMethod(info.InjectableMethods[i], target);
            }
        }
        private InjectableAttributesInfo GetInjectableAttributes(object target)
        {
            if (_configuration.CacheAttributes)
            {
                return AttributeCache.Get(target.GetType());
            }
            else
            {
                return InjectableAttributeProvider.Provide(target.GetType());
            }
        }

        private void InjectField(FieldInfo field, object target)
        {
            field.SetValue(target, _container.Resolve(field.FieldType));
        }

        private void InjectProperty(PropertyInfo property, object target)
        {
            property.SetValue(target, _container.Resolve(property.PropertyType));
        }

        private void InjectMethod(InjectMethodInfo method, object instance)
        {
            var arguments = TypedArrayPool<object>.Shared.Rent(method.Parameters.Length);
            try
            {
                for (var i = 0; i < method.Parameters.Length; i++)
                {
                    arguments[i] = _container.Resolve(method.Parameters[i].ParameterType);
                }

                method.MethodInfo.Invoke(instance, arguments);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                TypedArrayPool<object>.Shared.Return(arguments);
            }
        }
    }
}