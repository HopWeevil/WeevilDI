using UnityEngine;

namespace WeevilDI.Core
{
    public abstract class Installer : MonoBehaviour
    {
        [SerializeField] private bool _autoRun;
        public DIContainer DIContainer { get; private set; }

        private void Awake()
        {
            if (_autoRun)
            {
                Build();
            }
        }

        public void Build()
        {
            DIContainer = FindObjectOfType<DIContainer>();
            Install(DIContainer);
        }
        public abstract void Install(DIContainer container);
    }
}