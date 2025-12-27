using Ashking.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace Ashking.Systems
{
    public partial struct PlayerMovementSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            
            foreach (var (transform,velocity, moveSpeed, moveDirection, rotationSpeed, lookDirection) in
                     SystemAPI.Query<RefRW<LocalTransform>, RefRW<PhysicsVelocity>, PlayerMoveSpeed, MoveDirection, PlayerRotationSpeed, LookDirection>().WithAll<PlayerTag>())
            {
                var yaw = lookDirection.Value.x * rotationSpeed.Value * deltaTime;// Rotate around the Y-axis (for looking left/right)
                quaternion yawRotation = quaternion.AxisAngle(math.up(), yaw);// Calculate the rotation quaternion
                
                // Rotate player's transform
                transform.ValueRW.Rotation = math.mul(transform.ValueRO.Rotation, yawRotation);
                
                // Move player's transform 
                var deltaMovement = moveDirection.Value * moveSpeed.Value;
                velocity.ValueRW.Linear =  new float3(deltaMovement.x, 0, deltaMovement.y);
            }
        }
    }
}