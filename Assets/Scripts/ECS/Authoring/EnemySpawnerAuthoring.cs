using Ashking.Components;
using Unity.Entities;
using UnityEngine;

namespace Ashking.Authoring
{
    public class EnemySpawnerAuthoring : MonoBehaviour
    {
        [SerializeField] GameObject enemyPrefab;
        [SerializeField] float spawnInterval;
        [SerializeField] Transform spawnPoint;
        
        private class EnemySpawnerBaker : Baker<EnemySpawnerAuthoring>
        {
            public override void Bake(EnemySpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new EnemySpawnData
                {
                    EnemyPrefab = GetEntity(authoring.enemyPrefab, TransformUsageFlags.Dynamic),
                    SpawnInterval = authoring.spawnInterval,
                    SpawnPoint = authoring.spawnPoint
                });
                AddComponent<EnemySpawnTimer>(entity);
            }
        }
    }
}