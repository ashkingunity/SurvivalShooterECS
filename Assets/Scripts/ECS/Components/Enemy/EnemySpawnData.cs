using Unity.Entities;
using UnityEngine;

namespace Ashking.Components
{
    public struct EnemySpawnData : IComponentData
    {
        public Entity EnemyPrefab;
        public float SpawnInterval;
        public UnityObjectRef<Transform> SpawnPoint;
    }
}