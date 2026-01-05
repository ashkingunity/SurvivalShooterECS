using Ashking.Components;
using Ashking.OOP;
using Unity.Entities;
using Unity.Transforms;

namespace Ashking.Systems
{
    [UpdateAfter(typeof(TransformSystemGroup))]
    public partial struct PlayerGameObjectMovementSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (transform, playerGameObjectData, moveDirection) in
                     SystemAPI.Query<RefRW<LocalTransform>, RefRW<PlayerGameObjectData>, MoveDirection>().WithNone<InitializePlayerEntityTag>().WithAll<PlayerTag>())
            {
                // Rotate animator gameObject
                playerGameObjectData.ValueRW.Animator.Value.transform.rotation = transform.ValueRO.Rotation;

                // Move animator gameObject
                playerGameObjectData.ValueRW.Animator.Value.transform.position = transform.ValueRO.Position;
                
                // Update walking and idle animation
                bool isWalking = moveDirection.Value.x != 0 || moveDirection.Value.y != 0;
                playerGameObjectData.ValueRW.Animator.Value.SetBool(SurvivalShooterAnimationHashes.WalkingHash , isWalking);
            }
        }
    }
}