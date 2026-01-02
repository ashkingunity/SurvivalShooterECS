using Ashking.Components;
using Ashking.OOP;
using Unity.Entities;

namespace Ashking.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct EnemyTargetInitializationSystem : ISystem
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
                var enemyGameObject = EnemyProvider.Instance.GetEnemy();
                enemyGameObjectData.ValueRW.NavMeshAgent = enemyGameObject.navMeshAgent;
                enemyGameObjectData.ValueRW.Animator = enemyGameObject.animator;
                enemyGameObjectData.ValueRW.AudioSource = enemyGameObject.audioSource;
                enemyGameObjectData.ValueRW.HitParticles = enemyGameObject.hitParticles;
                
                ecb.RemoveComponent<InitializeEnemyGameObjectDataTag>(entity);
            }
        
            ecb.Playback(state.EntityManager);
        }
    }
}