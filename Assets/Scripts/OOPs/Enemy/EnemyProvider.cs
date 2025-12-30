using System.Collections.Generic;
using OOPs.Utlities;
using UnityEngine;

namespace Ashking.OOP
{
    public class EnemyProvider : Singleton<EnemyProvider>
    {
        public List<Enemy> enemies;

        Queue<Enemy> enemiesQueue;

        void Start()
        {
            enemiesQueue = new Queue<Enemy>(enemies);
        }

        public Enemy GetEnemy()
        {
            return enemiesQueue.Dequeue();
        }
    }
}