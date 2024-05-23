using UnityEngine;
using WeevilDI.Attributes;

namespace PlayModeTests
{
    public class MonoBehaviourFoo : MonoBehaviour
    {
        private IService _service;

        [Inject]
        public void Construct(IService service)
        {
            _service = service;
            _service.DoSomething();
        }
    }
}
