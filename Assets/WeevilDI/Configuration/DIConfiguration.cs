using UnityEngine;

namespace WeevilDI.Configuration
{
    [CreateAssetMenu(fileName = "WeevilDIConfiguration", menuName = "WeevilDI/Configuration", order = 1)]
    public class DIConfiguration : ScriptableObject
    {
        [field: SerializeField] public bool CacheAttributes { get; private set; }
        [field: SerializeField] public bool CacheConstructors { get; private set; }
    }
}
