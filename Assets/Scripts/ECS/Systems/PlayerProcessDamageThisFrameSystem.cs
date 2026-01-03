using Ashking.Components;
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

                var previousHealth = currentHealth.ValueRO.Value;
                // update currentHealth for each damage/hits taken this frame
                foreach (var damage in damageThisFrame)
                {
                    currentHealth.ValueRW.Value -= damage.Value;
                }
                
                // clear damageThisFrame buffer after process the damages/hits
                damageThisFrame.Clear();
                
                // Play player hurt audio only if player damaged
                if (!Mathf.Approximately(previousHealth, currentHealth.ValueRO.Value))
                {
                    PlayerGameObject.Instance.playerAudioSource.Play();
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