using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using WeevilDI.Injectors;
using WeevilDI.Configuration;

namespace WeevilDI.Core
{
    [DefaultExecutionOrder(-1)]
    public class DIContainer : MonoBehaviour
    {
        [SerializeField] private DIConfiguration _configuration;

        private DependencyRegistry _registry;
        private ReflectionInjector _reflectionInjector;
        private GameObjectInjector _gameObjectInjector;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _registry = new DependencyRegistry();
            _reflectionInjector = new ReflectionInjector(this, _configuration);
            _gameObjectInjector = new GameObjectInjector(_reflectionInjector);
            DontDestroyOnLoad(this);
        }

        private void OnEnable()
        {
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= OnActiveSceneChanged;
        }

        private void OnActiveSceneChanged(Scene arg0, Scene arg1)
        {
            _registry.RemoveDependencies(Lifetime.Scene);
        }

        public void Register<T>(T dependency, Lifetime lifetime = Lifetime.Global)
        {
            _registry.Add(dependency, lifetime);
        }

        public void RegisterComponentInHierarchy<T>() where T : Component
        {
            T component = FindObjectOfType<T>();
            if (component != null)
            {
                Register(component, Lifetime.Scene);
                _gameObjectInjector.InjectComponent<T>(component.gameObject);
            }
            else
            {
                throw new Exception($"No component of type {typeof(T)} found in hierarchy.");
            }
        }

        public void RegisterComponentFromPrefab<T>(T prefab) where T : Component
        {
            var wasActive = prefab.gameObject.activeSelf;
            prefab.gameObject.SetActive(false);
            var instance = Instantiate(prefab);

            try
            {
                Register(instance, Lifetime.Scene);
                _gameObjectInjector.Inject(instance.gameObject);
            }
            finally
            {
                prefab.gameObject.SetActive(wasActive);
                instance.gameObject.SetActive(wasActive);
            }
        }

        public T Resolve<T>()
        {
            if (_registry.TryGetDependency(typeof(T), out var dependency))
            {
                return (T)dependency;
            }
            throw new Exception($"No dependency found for type: {typeof(T)}");
        }

        public object Resolve(Type type)
        {
            if (_registry.TryGetDependency(type, out var dependency))
            {
                return dependency;
            }
            throw new Exception($"No dependency found for type: {type}");
        }

        public T InstantiatePrefabFromComponent<T>(T prefab) where T : Component
        {
            /*T inst = Instantiate(prefab);
            ReflectionInjector.InjectAttributes(inst, this, _useCache);
            return inst;*/
            var wasActive = prefab.gameObject.activeSelf;

            prefab.gameObject.SetActive(false);

            var instance = Instantiate(prefab);
            try
            {
                _gameObjectInjector.Inject(instance.gameObject);
            }
            finally
            {
                prefab.gameObject.SetActive(wasActive);
                instance.gameObject.SetActive(wasActive);
            }

            return instance;
        }

        public T Construct<T>()
        {
            return (T)_reflectionInjector.CreateInstance(typeof(T));
        }

        public void Inject(object target)
        {
            _reflectionInjector.InjectAttributes(target);
        }
    }
}