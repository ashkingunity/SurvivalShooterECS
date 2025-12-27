using Ashking.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Ashking.Systems
{
    public partial struct PlayerMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            
            foreach (var (transform, moveSpeed, moveDirection, rotationSpeed, lookDirection) in SystemAPI.Query<RefRW<LocalTransform>, PlayerMoveSpeed, MoveDirection, PlayerRotationSpeed, LookDirection>())
            {
                var yaw = lookDirection.Value.x * rotationSpeed.Value * deltaTime;// Rotate around the Y-axis (for looking left/right)
                quaternion yawRotation = quaternion.AxisAngle(math.up(), yaw);// Calculate the rotation quaternion
                
                // Rotate player's transform
                transform.ValueRW.Rotation = math.mul(transform.ValueRO.Rotation, yawRotation);
                
                // Move player's transform 
                var deltaMovement = moveDirection.Value * moveSpeed.Value * deltaTime;
                transform.ValueRW.Position +=  new float3(deltaMovement.x, 0, deltaMovement.y);
            }
        }
    }
}