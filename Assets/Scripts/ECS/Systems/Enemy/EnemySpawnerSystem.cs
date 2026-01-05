using Ashking.Components;
using Unity.Entities;
using Unity.Transforms;

namespace Ashking.Systems
{
    public partial struct EnemySpawnerSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSystem = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSystem.CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var (enemyNextSpawnTime, enemySpawnData)
                     in SystemAPI.Query<RefRW<EnemySpawnTimer>, EnemySpawnData>())
            {
                enemyNextSpawnTime.ValueRW.Value -= deltaTime;
                if (enemyNextSpawnTime.ValueRO.Value > 0f) 
                    continue;

                enemyNextSpawnTime.ValueRW.Value = enemySpawnData.SpawnInterval;
                
                var newEnemy = ecb.Instantiate(enemySpawnData.EnemyPrefab);// Instantiate enemy entity
                ecb.SetComponent(newEnemy, LocalTransform.FromPosition(enemySpawnData.SpawnPoint.Value.position));// Set its spawn position
            }
        }
    }
}