using Ashking.Components;
using Ashking.OOP;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
using RaycastHit = Unity.Physics.RaycastHit;

namespace Ashking.Systems
{
    public partial class PlayerMovementSystem : SystemBase
    {
        Camera _camera;

        protected override void OnCreate()
        {
            _camera = Camera.main;
        }
        
        protected override void OnUpdate()
        {
            foreach (var (transform, velocity, moveSpeed, rotationSpeed, moveDirection, lookDirection) in
                     SystemAPI.Query<RefRW<LocalTransform>, RefRW<PhysicsVelocity>, MoveSpeed, RotationSpeed, MoveDirection, LookDirection>().WithAll<PlayerTag>())
            {
                if (PerformRaycast(out RaycastHit raycastHit, lookDirection.Value))
                {
                    float3 directionToFace = raycastHit.Position - transform.ValueRO.Position;
                    float3 upVector = new float3(0f, 1f, 0f);
                    
                    // Rotate player's transform
                    transform.ValueRW.Rotation = quaternion.LookRotation(directionToFace, upVector);
                }
                
                // Move player's transform 
                var deltaMovement = moveDirection.Value * moveSpeed.Value;
                velocity.ValueRW.Linear =  new float3(deltaMovement.x, 0, deltaMovement.y);
            }
        }

        bool PerformRaycast(out RaycastHit raycastHit, Vector2 mousePosition)
        {
            var ray =  _camera.ScreenPointToRay(mousePosition);
            RaycastInput rayCastInput = new RaycastInput
            {
                Start = ray.origin,
                End =  ray.GetPoint(100f),
                Filter = new CollisionFilter
                {
                    BelongsTo = 1 << (int)CustomCollisionLayerNameEnum.Player ,
                    CollidesWith =  1 << (int)CustomCollisionLayerNameEnum.Floor
                }
            };
            
            PhysicsWorldSingleton physicsWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
            return physicsWorld.CastRay(rayCastInput, out raycastHit);
        }
    }
}