using UnityEngine;
namespace WeevilDI.Injectors
{
    public class GameObjectInjector
    {
        private readonly ReflectionInjector _reflectionInjector;

        public GameObjectInjector(ReflectionInjector reflectionInjector)
        {
            _reflectionInjector = reflectionInjector;
        }

        public void InjectSingle(GameObject gameObject)
        {
            MonoBehaviour[] monoBehaviours =  gameObject.GetComponents<MonoBehaviour>();

            for (int i = 0; i < monoBehaviours.Length; i++)
            {
                _reflectionInjector.InjectAttributes(monoBehaviours[i]);                
            }
        }

        public void Inject(GameObject gameObject)
        {
            MonoBehaviour[] monoBehaviours = gameObject.GetComponentsInChildren<MonoBehaviour>();

            for (int i = 0; i < monoBehaviours.Length; i++)
            {
                _reflectionInjector.InjectAttributes(monoBehaviours[i]);
            }
        }

        public void InjectComponent<T>(GameObject gameObject) where T : Component
        {
            _reflectionInjector.InjectAttributes(gameObject.GetComponent<T>());           
        }
    }
}
