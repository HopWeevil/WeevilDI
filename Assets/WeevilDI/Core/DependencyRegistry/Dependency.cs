namespace WeevilDI.Core
{
    public class Dependency
    {
        public readonly object Instance;
        public readonly Lifetime Lifetime;

        public Dependency(object instance, Lifetime lifetime)
        {
            Instance = instance;
            Lifetime = lifetime;
        }
    }
}