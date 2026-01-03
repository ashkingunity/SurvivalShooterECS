using System.Collections.Generic;
using OOPs.Utlities;
using UnityEngine;

namespace Ashking.OOP
{
    public class EnemyProvider : Singleton<EnemyProvider>
    {
        [SerializeField] List<EnemyGameObject> enemyGameObjects;
        Dictionary<string, IPoolable> poolsDictionary = new();
        bool isInitializing = false;

        new void Awake()
        {
            base.Awake();
            
            if(Instance != this)
                return;

            Initialize();
        }
        
        public GameObject GetEnemy(EnemyName enemyName)
        {
            if (isInitializing == false)
            {
                Initialize();
            }
            
            var poolable = poolsDictionary[enemyName.ToString()];
            return poolable != null ? Pool.Instance.GetObjectFromPool(poolable, enemyName.ToString()) : null;
        }
        
        public void ReturnEnemy(GameObject gObj, EnemyName enemyName)
        {
            if (poolsDictionary.ContainsKey(enemyName.ToString()))
            {
                Pool.Instance.AddBackToPool(gObj, enemyName.ToString());
            }
        }

        void Initialize()
        {
            isInitializing = true;

            foreach (var enemyGameObject in enemyGameObjects)
            {
                if (enemyGameObject.TryGetComponent(out IPoolable poolable))
                {
                    poolsDictionary[enemyGameObject.enemyName.ToString()] = poolable;
                }
            }
        }
    }
}