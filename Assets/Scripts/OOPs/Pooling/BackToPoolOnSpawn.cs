using UnityEngine;

namespace Ashking.OOP
{
    [RequireComponent(typeof(Poolable))]
    public class BackToPoolOnSpawn : MonoBehaviour
    {
        [SerializeField] float returnTime = 1.5f;
        IPoolable poolable;
        void Awake()
        {
            poolable = GetComponent<IPoolable>();
        }

        void OnEnable()
        {
            CancelInvoke("ReturnBackToPool");
            if (poolable != null)
            {
                Invoke("ReturnBackToPool", returnTime);
            }
        }

        void ReturnBackToPool()
        {
            if (poolable != null && poolable.PoolReference && gameObject.activeInHierarchy == true)
            {
                poolable.PoolReference.AddBackToPool(gameObject);
            }
        }
    }

}