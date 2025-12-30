using Ashking.Components;
using Ashking.OOP;
using Unity.Entities;
using UnityEngine;

namespace Ashking.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct EnemyTargetInitializationSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<InitializeEnemyTargetTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            if (EnemyProvider.Instance == null)
                return;

            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
            foreach (var (enemyTarget, entity) in SystemAPI.Query<RefRW<EnemyTarget>>()
                         .WithAll<InitializeEnemyTargetTag, EnemyTag>().WithEntityAccess())
            {
                enemyTarget.ValueRW.Enemy = EnemyProvider.Instance.GetEnemy();
                ecb.RemoveComponent<InitializeEnemyTargetTag>(entity);
            }
        
            ecb.Playback(state.EntityManager);
        }
    }
}