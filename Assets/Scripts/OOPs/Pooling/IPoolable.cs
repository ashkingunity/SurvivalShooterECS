using UnityEngine;

namespace Ashking.OOP
{
    // An interface to be implemented by Poolable gameobjects
    public interface IPoolable
    {
        int PoolSize { get; }
        GameObject ObjectToPool{ get; }
        Pool PoolReference { get; set; }// used to return gameobject back to its pool
    }
}
