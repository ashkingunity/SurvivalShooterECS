using Ashking.Components;
using Ashking.OOP;
using Unity.Entities;
using Unity.Transforms;

namespace Ashking.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct EnemyGameObjectDataInitializationSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<InitializeEnemyGameObjectDataTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            if (EnemyProvider.Instance == null)
                return;

            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
            foreach (var (enemyGameObjectData, entity) in SystemAPI.Query<RefRW<EnemyGameObjectData>>()
                         .WithAll<InitializeEnemyGameObjectDataTag, EnemyTag>().WithEntityAccess())
            {
                var gameObject = EnemyProvider.Instance.GetEnemy(enemyGameObjectData.ValueRO.EnemyName);// Instantiate enemy gameObject
                if (gameObject!= null && gameObject.TryGetComponent(out EnemyGameObject enemyGameObject))
                {
                    gameObject.transform.position = SystemAPI.GetComponent<LocalTransform>(entity).Position;// Set spawn positon of enemy gameObject
                        
                    enemyGameObjectData.ValueRW.NavMeshAgent = enemyGameObject.navMeshAgent;
                    enemyGameObjectData.ValueRW.Animator = enemyGameObject.animator;
                    enemyGameObjectData.ValueRW.AudioSource = enemyGameObject.audioSource;
                    enemyGameObjectData.ValueRW.HitParticles = enemyGameObject.hitParticles;
                
                    ecb.RemoveComponent<InitializeEnemyGameObjectDataTag>(entity);
                }
            }
        
            ecb.Playback(state.EntityManager);
        }
    }
}