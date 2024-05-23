
using UnityEngine;

namespace PlayModeTests
{
    public class ServiceB : IService
    {
        public void DoSomething()
        {
            Debug.Log("ServiceB");
        }
    }
}
