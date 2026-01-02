using System.Collections.Generic;
using UnityEngine;

namespace Ashking.OOP
{
  public class Pool : MonoBehaviour
  {
    [HideInInspector] public GameObject objectPrefab;
    Queue<GameObject> pooledObjects;
    public static Dictionary<string, Pool> pools = new Dictionary<string, Pool>();

    [HideInInspector] public GameObject parentTransform;// Just to keep hierarchy clean
    Pool pool;

    private static Pool instance;
    public static Pool Instance
    {
      get
      {
        if (instance == null)
        {
          GameObject pool = new GameObject("Pool");
          instance = pool.AddComponent<Pool>();
        }
        return instance;
      }
    }

    void OnDestroy()
    {
      if (instance != null)
      {
        instance = null;
        pools = new Dictionary<string, Pool>();// Clear old pools when gameplay scene is exited to prevent getting null gameobjects from pool when gameplay scene is reloaded
      }
    }

    public GameObject GetObjectFromPool(IPoolable poolable)
    {
      if (pools.ContainsKey(poolable.ObjectToPool.name))
      {
        Pool pool = pools[poolable.ObjectToPool.name];
        return pool.GetGameObject();
      }
      else
      {
        CreatePool(poolable);
        Pool pool = pools[poolable.ObjectToPool.name];
        return pool.GetGameObject();
      }
    }

    public void AddBackToPool(GameObject gObj)
    {
      gObj.SetActive(false);
      pooledObjects.Enqueue(gObj);
    }

    void CreatePool(IPoolable poolable)
    {
      GameObject gObj = new GameObject(poolable.ObjectToPool.name);

      pool = gObj.AddComponent<Pool>();
      pool.parentTransform = gObj;
      pool.objectPrefab = poolable.ObjectToPool;
      pool.AddToPool(poolable);
      pools.Add(poolable.ObjectToPool.name, pool);
    }

    void AddToPool(IPoolable poolable)
    {
      pooledObjects = new Queue<GameObject>();
      for (int i = 0; i < poolable.PoolSize; i++)
      {
        CreateObject();
      }
    }

    void CreateObject()
    {
      GameObject go = Instantiate(objectPrefab);
      go.GetComponent<IPoolable>().PoolReference = this;// Assign/Initialize PoolReference property of instantiated poolable gameobject
      go.transform.SetParent(parentTransform.transform);
      go.SetActive(false);
      pooledObjects.Enqueue(go);
    }


    GameObject GetGameObject()
    {
      if (pooledObjects.Count > 0)
      {
        GameObject go = pooledObjects.Peek();
        if (go.activeInHierarchy == false)
        {
          go = pooledObjects.Dequeue();
          go.SetActive(true);
          return go;
        }
        else
        {
          CreateObject();
          go = pooledObjects.Dequeue();
          go.SetActive(true);
          return go;
        }
      }
      else
      {
        CreateObject();
        GameObject go = pooledObjects.Dequeue();
        go.SetActive(true);
        return go;
      }
    }
  }

}