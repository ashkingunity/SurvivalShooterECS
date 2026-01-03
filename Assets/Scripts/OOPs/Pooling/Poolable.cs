using UnityEngine;

namespace Ashking.OOP
{
  // A class to be attached to a gameobject for it to use Object Pooling system
  [DisallowMultipleComponent]
  public class Poolable : MonoBehaviour, IPoolable
  {
    [SerializeField] int poolSize = 5;
    public int PoolSize => poolSize;
    public GameObject ObjectToPool => gameObject;
    public Pool PoolReference { get; set; }
  }

}
