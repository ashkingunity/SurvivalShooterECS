using Ashking.Components;
using Ashking.OOP;
using AshKing.OOP;
using Unity.Entities;
using UnityEngine;

namespace Ashking.Systems
{
    public partial struct PlayerProcessDamageThisFrameSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (currentHealth, damageThisFrame, entity) 
                     in SystemAPI.Query<RefRW<CurrentHealth>, DynamicBuffer<DamageThisFrame>>()
                         .WithAll<PlayerTag>().WithPresent<DestroyEntityFlag>().WithEntityAccess())
            {
                // skip if no damage/hits is taken this frame
                if(damageThisFrame.IsEmpty)
                    continue;

                var hitPointsThisFrame = 0;
                // add accumulated damage/hits taken this frame
                foreach (var damage in damageThisFrame)
                {
                    hitPointsThisFrame += damage.Value;
                }

                // reduce accumulated damage/hits from currentHealth
                currentHealth.ValueRW.Value -= hitPointsThisFrame;
                
                // clear damageThisFrame buffer after process the damages/hits
                damageThisFrame.Clear();
                
                // Play player hurt audio only if player damaged
                if (hitPointsThisFrame > 0)
                {
                    PlayerGameObject.Instance.playerAudioSource.Play();
                    GameUIController.Instance.OnPlayerTookDamage(currentHealth.ValueRO.Value);
                }

                if (currentHealth.ValueRW.Value <= 0)
                {
                    if (SystemAPI.HasComponent<PlayAudioClipOnDestroy>(entity))
                    {
                        // To play player death audio
                        SystemAPI.SetComponentEnabled<PlayAudioClipOnDestroy>(entity, true);
                    }
                    
                    // Enable DestroyEntityFlag
                    SystemAPI.SetComponentEnabled<DestroyEntityFlag>(entity, true);
                }
            }
        }
    }
}