using WeevilDI.Attributes;

namespace PlayModeTests
{
    public class Foo
    {
        [Inject] public readonly int InjectedFieldValue;
        [Inject] public int InjectedPropertyValue { get; private set; }
        public int InjectedMethodValue { get; private set; }


        [Inject]
        private void Inject(int value)
        {
            InjectedMethodValue = value;
        }
    }
}