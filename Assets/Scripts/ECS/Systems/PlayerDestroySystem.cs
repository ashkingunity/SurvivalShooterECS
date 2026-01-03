using Ashking.Components;
using Ashking.OOP;
using Unity.Entities;

namespace Ashking.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    [UpdateBefore(typeof(EndSimulationEntityCommandBufferSystem))]
    public partial struct PlayerDestroySystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var endEcbSystem = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var endEcb = endEcbSystem.CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var (playerGameObjectData, _, entity) in SystemAPI.Query<RefRW<PlayerGameObjectData>, DestroyEntityFlag>().WithAll<PlayerTag>().WithEntityAccess())
            {
                playerGameObjectData.ValueRW.Animator.Value.SetTrigger(SurvivalShooterAnimationHashes.DieHash);
                    
                endEcb.DestroyEntity(entity);
            }
        }
    }
}