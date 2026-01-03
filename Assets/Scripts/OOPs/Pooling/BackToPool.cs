using UnityEngine;

namespace Ashking.OOP
{
  [RequireComponent(typeof(Poolable))]
  public class BackToPool : MonoBehaviour
  {
    IPoolable poolable;
    void Awake()
    {
      poolable = GetComponent<IPoolable>();
    }

    void OnEnable()
    {
      CancelInvoke(nameof(ReturnBackToPool));
    }

    public void ReturnToPool(float time)
    {
      CancelInvoke(nameof(ReturnBackToPool));
      if (poolable != null)
      {
        Invoke(nameof(ReturnBackToPool), time);
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