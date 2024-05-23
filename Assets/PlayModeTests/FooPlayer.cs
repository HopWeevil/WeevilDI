using PlayModeTests;

namespace PlayModeTests
{
    public class FooPlayer
    {
        public readonly IService service;

        public FooPlayer(IService service)
        {
            this.service = service;
            this.service.DoSomething();
        }
    }
}
