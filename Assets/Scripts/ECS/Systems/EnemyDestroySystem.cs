using Ashking.Components;
using Ashking.OOP;
using Unity.Entities;

namespace Ashking.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    [UpdateBefore(typeof(EndSimulationEntityCommandBufferSystem))]
    public partial struct EnemyDestroySystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var endEcbSystem = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var endEcb = endEcbSystem.CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (enemyGameObjectData, _, entity) 
                     in SystemAPI.Query<RefRW<EnemyGameObjectData>, DestroyEntityFlag>().WithAll<EnemyTag>().WithEntityAccess())
            {
                enemyGameObjectData.ValueRW.Animator.Value.SetTrigger(SurvivalShooterAnimationHashes.DieHash);

                EnemyProvider.Instance.ReturnEnemy(enemyGameObjectData.ValueRW.Animator.Value.gameObject,
                    enemyGameObjectData.ValueRW.EnemyName);
                    
                endEcb.DestroyEntity(entity);
            }
        }
    }
}