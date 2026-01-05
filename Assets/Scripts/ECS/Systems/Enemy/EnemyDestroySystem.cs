using Ashking.Components;
using Ashking.Groups;
using Ashking.OOP;
using Unity.Entities;

namespace Ashking.Systems
{
    [UpdateInGroup(typeof(EntityDestructionGroup))]
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
                enemyGameObjectData.ValueRW.NavMeshAgent.Value.enabled = false;
                    
                endEcb.DestroyEntity(entity);
            }
        }
    }
}