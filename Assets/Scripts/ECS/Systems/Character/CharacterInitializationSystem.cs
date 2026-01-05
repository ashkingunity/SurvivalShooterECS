using Ashking.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

namespace Ashking.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct CharacterInitializationSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (mass, shouldInitialize) in SystemAPI.Query<RefRW<PhysicsMass>, EnabledRefRW<InitializeCharacterFlag>>())
            {
                mass.ValueRW.InverseInertia = float3.zero;// Modify the InverseInertia to lock rotation axes
                shouldInitialize.ValueRW = false;
            }
        }
    }
}