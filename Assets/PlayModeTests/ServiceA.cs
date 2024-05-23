
using UnityEngine;

namespace PlayModeTests
{
    public class ServiceA : IService
    {
        public void DoSomething()
        {
            Debug.Log("ServiceA");
        }
    }
}
