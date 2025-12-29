using Ashking.Components;
using AshKing.Groups;
using Unity.Entities;
using Unity.Physics;
using UnityEngine;
using Ray = Unity.Physics.Ray;
using RaycastHit = Unity.Physics.RaycastHit;

namespace Ashking.Systems
{
    [UpdateInGroup(typeof(PlayerShootingGroup))]
    public partial class PlayerShootingSystem : SystemBase
    {
        Ray _shootRay;
        
        protected override void OnUpdate()
        {
            foreach (var (playerShootingEffectsData, playerShootingTimer, playerShootingData, canShoot) 
                     in SystemAPI.Query<RefRW<PlayerShootingEffectsData>, RefRW<PlayerShootingTimer>, PlayerShootingData, CanShoot>())
            {
                if (!canShoot.Value || playerShootingTimer.ValueRO.Value < playerShootingData.TimeBetweenShots)
                {
                    return;
                }
                
                // Reset the timer.
                playerShootingTimer.ValueRW.Value = 0f;
                
                // Enable the lights.
                playerShootingEffectsData.ValueRW.GunLight.Value.enabled = true;
                playerShootingEffectsData.ValueRW.FaceLight.Value.enabled = true;

                // Stop the particles from playing if they were, then start the particles.
                playerShootingEffectsData.ValueRW.GunParticles.Value.Stop();
                playerShootingEffectsData.ValueRW.GunAudio.Value.Play();
                
                // Enable the line renderer and set it's first position to be the end of the gun.
                playerShootingEffectsData.ValueRW.GunLine.Value.enabled = true;
                playerShootingEffectsData.ValueRW.GunLine.Value.SetPosition (0,  playerShootingEffectsData.ValueRO.GunTipTransform.Value.position);
                
                _shootRay.Origin = playerShootingEffectsData.ValueRO.GunTipTransform.Value.position;
                _shootRay.Displacement = playerShootingEffectsData.ValueRO.GunTipTransform.Value.forward * playerShootingData.ShootingRange;
                
                RaycastInput rayCastInput = new RaycastInput
                {
                    Start = _shootRay.Origin,
                    End = _shootRay.Displacement,
                    Filter = new CollisionFilter
                    {
                        BelongsTo = (uint)LayerMask.GetMask("Player"),
                        CollidesWith =  (uint) playerShootingData.ShootableMask
                    }
                };
                
                PhysicsWorldSingleton physicsWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
                if (physicsWorld.CastRay(rayCastInput, out RaycastHit raycastHit))
                {
                    // Set the second position of the line renderer to the point the raycast hit.
                    playerShootingEffectsData.ValueRW.GunLine.Value.SetPosition(1, raycastHit.Position);
                }
                else
                {
                    // Set the second position of the line renderer to the fullest extent of the gun's range.
                    playerShootingEffectsData.ValueRW.GunLine.Value.SetPosition(1, _shootRay.Origin + _shootRay.Displacement);
                }
            }
        }
    }
}