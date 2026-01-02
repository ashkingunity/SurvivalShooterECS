using Ashking.Components;
using Ashking.Groups;
using Ashking.OOP;
using Unity.Entities;
using Unity.Physics;
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
            foreach (var (playerShootingEffectsData, shootingTimer, playerShootingData, canShoot) 
                     in SystemAPI.Query<RefRW<PlayerShootingEffectsData>, RefRW<ShootingTimer>, PlayerShootingData, CanShoot>().WithAll<PlayerTag>())
            {
                if (!canShoot.Value || shootingTimer.ValueRO.Value < playerShootingData.TimeBetweenShots)
                {
                    return;
                }
                
                // Reset the timer.
                shootingTimer.ValueRW.Value = 0f;
                
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
                        BelongsTo = 1 << (int) CustomCollisionLayerNameEnum.Player,
                        CollidesWith =  (uint) playerShootingData.ShootableMask
                    },
                    
                };
                
                PhysicsWorldSingleton physicsWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
                if (physicsWorld.CastRay(rayCastInput, out RaycastHit raycastHit))
                {
                    // Set the second position of the line renderer to the point the raycast hit.
                    playerShootingEffectsData.ValueRW.GunLine.Value.SetPosition(1, raycastHit.Position);

                    var hitEntity = raycastHit.Entity;
                    if (SystemAPI.HasComponent<CurrentHealth>(hitEntity))
                    {
                        var currentHealth = SystemAPI.GetComponentRW<CurrentHealth>(hitEntity);
                        currentHealth.ValueRW.Value -=  playerShootingData.DamagePerShot;
                    }

                    if (SystemAPI.HasComponent<EnemyGameObjectData>(hitEntity))
                    {
                        var enemyGameObjectData = SystemAPI.GetComponentRW<EnemyGameObjectData>(hitEntity);
                        enemyGameObjectData.ValueRW.AudioSource.Value.Play();
                        enemyGameObjectData.ValueRW.HitParticles.Value.transform.position = raycastHit.Position;
                        enemyGameObjectData.ValueRW.HitParticles.Value.Play();
                    }
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