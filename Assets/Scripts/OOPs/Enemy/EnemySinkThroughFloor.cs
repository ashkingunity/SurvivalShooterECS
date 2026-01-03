using UnityEngine;
using UnityEngine.Events;

namespace Ashking.OOP
{
    public class EnemySinkThroughFloor : MonoBehaviour
    {
        public float sinkSpeed = 2.5f; // The speed at which the enemy sinks through the floor when dead
        bool isSinking; // Whether the enemy has started sinking through the floor.
        
        [field: SerializeField]
        public UnityEvent<EnemySinkThroughFloor> OnSink { get; private set; }

        void OnEnable()
        {
            isSinking = false;
        }

        void OnDisable()
        {
            isSinking = false;
        }

        void Update()
        {
            // If the enemy should be sinking...
            if (isSinking)
            {
                // ... move the enemy down by the sinkSpeed per second.
                transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
            }
        }
        
        public void StartSinking()
        {
            // The enemy should on sink.
            isSinking = true;
            
            OnSink?.Invoke(this);
        }
    }
}

