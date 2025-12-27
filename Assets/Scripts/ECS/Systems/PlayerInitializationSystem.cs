using Ashking.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

namespace Ashking.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct PlayerInitializationSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (mass, shouldInitialize) in SystemAPI.Query<RefRW<PhysicsMass>, EnabledRefRW<InitializePlayerFlag>>())
            {
                mass.ValueRW.InverseInertia = float3.zero;// Modify the InverseInertia to lock rotation axes
                shouldInitialize.ValueRW = false;
            }
        }
    }
}