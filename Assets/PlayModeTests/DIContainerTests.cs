using NUnit.Framework;
using PlayModeTests;
using UnityEditor;
using UnityEngine;
using WeevilDI.Core;

public class DIContainerTests
{

    [Test]
    public void WhenServiceRegistered_AndResolved_ThenShouldReturnRegisteredService()
    {
        DIContainer container = Object.Instantiate(LoadDI());
        ServiceB serviceB = new ServiceB();

        container.Register<IService>(serviceB);
        IService resolvedService = container.Resolve<IService>();

        Assert.IsNotNull(resolvedService, "Resolved service should not be null");
        Assert.AreSame(serviceB, resolvedService, "Resolved service should be the same instance as the registered service");
    }

    [Test]
    public void WhenServiceRegistered_AndInstanceConstructed_ThenShouldInjectAllAtributesMarkedWithInjectAttribute()
    {
        DIContainer container = Object.Instantiate(LoadDI());

        container.Register(88);
        Foo foo = container.Construct<Foo>();

        Assert.IsNotNull(foo, "Foo instance should not be null");
        Assert.AreEqual(88, foo.InjectedFieldValue, "Injected field value should be 88");
        Assert.AreEqual(88, foo.InjectedPropertyValue, "Injected property value should be 88");
        Assert.AreEqual(88, foo.InjectedMethodValue, "Injected method value should be 88");
    }

    [Test]
    public void WhenServiceRegistered_AndInstanceConstructed_ThenShouldInjectConstructor()
    {
        DIContainer container = Object.Instantiate(LoadDI());
        ServiceA serviceA = new ServiceA();

        container.Register<IService>(serviceA);
        FooPlayer player = container.Construct<FooPlayer>();

        Assert.IsNotNull(player, "FooPlayer instance should not be null");
        Assert.AreEqual(serviceA, player.service, "Injected service value should be the same instance as the registered service");
    }


    [Test]
    public void WhenServiceRegistered_AndPrefabInstantiated_ThenShouldInjectDependenciesIntoPrefabInstance()
    {
        DIContainer container = Object.Instantiate(LoadDI());
        MonoBehaviourFoo foo = LoadMonoBehaviourFoo();

        container.Register<IService>(new ServiceA());
        MonoBehaviourFoo instance = container.InstantiatePrefabFromComponent(foo);

        Assert.IsNotNull(instance);
    }

    [Test]
    public void WhenComponentRegisteredInHierarchy_AndResolved_ThenShouldReturnComponentFromHierarchy()
    {
        DIContainer container = Object.Instantiate(LoadDI());
        GameObject gameObject = new GameObject();
        Camera registration = gameObject.AddComponent<Camera>();

        container.RegisterComponentInHierarchy<Camera>();
        Camera camera = container.Resolve<Camera>();

        Assert.IsNotNull(camera, "Camera instance should not be null");
        Assert.AreSame(camera, registration, "Resolved instance should be the same instance as the registered service");
    }

    private DIContainer LoadDI()
    {
        return AssetDatabase.LoadAssetAtPath<DIContainer>("Assets/PlayModeTests/Resources/DIContainer.prefab");
    }

    private MonoBehaviourFoo LoadMonoBehaviourFoo()
    {
        return AssetDatabase.LoadAssetAtPath<MonoBehaviourFoo>("Assets/PlayModeTests/Resources/MonoBehaviourFoo.prefab");
    }
}