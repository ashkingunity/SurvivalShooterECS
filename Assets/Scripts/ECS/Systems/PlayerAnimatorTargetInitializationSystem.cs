using Ashking.Components;
using AshKing.OOP;
using Unity.Entities;

namespace Ashking.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct PlayerAnimatorTargetInitializationSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<InitializePlayerAnimatorTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            if (PlayerGameObject.Instance == null)
                return;

            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
            foreach (var (playerAnimatorTarget, entity) in SystemAPI.Query<RefRW<AnimatorTarget>>()
                         .WithAll<InitializePlayerAnimatorTag, PlayerTag>().WithEntityAccess())
            {
                playerAnimatorTarget.ValueRW.Animator = PlayerGameObject.Instance.animator;
                ecb.RemoveComponent<InitializePlayerAnimatorTag>(entity);
            }
        
            ecb.Playback(state.EntityManager);
        }
    }
}