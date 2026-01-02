using System.Collections.Generic;
using OOPs.Utlities;
using UnityEngine;

namespace Ashking.OOP
{
    public class EnemyProvider : Singleton<EnemyProvider>
    {
        public List<EnemyGameObject> enemies;

        Queue<EnemyGameObject> enemiesQueue;

        void Start()
        {
            enemiesQueue = new Queue<EnemyGameObject>(enemies);
        }

        public EnemyGameObject GetEnemy()
        {
            return enemiesQueue.Dequeue();
        }
    }
}