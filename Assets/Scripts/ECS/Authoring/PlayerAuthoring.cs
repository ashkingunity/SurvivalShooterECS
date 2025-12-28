using Ashking.Components;
using Unity.Entities;
using UnityEngine;

namespace Ashking.Authoring
{
    public class PlayerAuthoring : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 3.5f;
        [SerializeField] float lookSpeed = 2;
        
        [Header("Shooting Data")]
        [SerializeField] int damagePerShot = 20; // The damage inflicted by each bullet
        [SerializeField] float timeBetweenShots = 0.15f; // The time between each shot
        [SerializeField] float shootingRange = 100f; // The distance the gun can fire
        [SerializeField] LayerMask shootableLayer; // A layer mask so the raycast only hits things on the shootable layer.

        private class PlayerAuthoringBaker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new PlayerMoveSpeed
                {
                    Value = authoring.moveSpeed
                });
                AddComponent(entity, new PlayerRotationSpeed
                {
                    Value = authoring.lookSpeed
                });
                AddComponent<PlayerTag>(entity);
                AddComponent<InitializePlayerFlag>(entity);
                
                AddComponent<MoveDirection>(entity);
                AddComponent<LookDirection>(entity);
                AddComponent<CanShoot>(entity);
                
                AddComponent<InitializeCameraTargetTag>(entity);
                AddComponent<CameraTarget>(entity);
                
                AddComponent<InitializePlayerAnimatorTag>(entity);
                AddComponent<PlayerAnimatorTarget>(entity);
                
                AddComponent(entity, new PlayerShootingData
                {
                    DamagePerShot = authoring.damagePerShot,
                    TimeBetweenShots = authoring.timeBetweenShots,
                    ShootingRange = authoring.shootingRange,
                    ShootableMask = authoring.shootableLayer
                });
                AddComponent<InitializePlayerShootingTag>(entity);
                AddComponent<PlayerShootingEffectsData>(entity);
            }
        }
    }
}