using Ashking.Components;
using AshKing.OOP;
using Unity.Entities;
using Unity.Transforms;

namespace Ashking.Systems
{
    [UpdateAfter(typeof(TransformSystemGroup))]
    public partial struct PlayerMoveAnimatorSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (transform, animatorTarget, moveDirection) in
                     SystemAPI.Query<RefRW<LocalTransform>, RefRW<PlayerAnimatorTarget>, MoveDirection>().WithNone<InitializePlayerAnimatorTag>().WithAll<PlayerTag>())
            {
                // Rotate animator gameObject
                animatorTarget.ValueRW.Animator.Value.transform.rotation = transform.ValueRO.Rotation;

                // Move animator gameObject
                animatorTarget.ValueRW.Animator.Value.transform.position = transform.ValueRO.Position;
                
                // Update walking and idle animation
                bool isWalking = moveDirection.Value.x != 0 || moveDirection.Value.y != 0;
                animatorTarget.ValueRW.Animator.Value.SetBool("isWalking", isWalking);
            }
        }
    }
}