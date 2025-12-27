using UnityEngine;

namespace OOPs.Utlities
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;

        void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning($"Warning multiple instances of {Instance.GetType().Name} detected. Destroying new instance.", Instance);
                Destroy(gameObject);
                return;
            }

            Instance = this as T;
        }
    }
}