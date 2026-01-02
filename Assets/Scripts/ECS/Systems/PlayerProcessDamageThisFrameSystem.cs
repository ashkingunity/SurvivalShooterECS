using Ashking.Components;
using AshKing.OOP;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace Ashking.Systems
{
    public partial struct PlayerProcessDamageThisFrameSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (currentHealth, damageThisFrame, entity) 
                     in SystemAPI.Query<RefRW<CurrentHealth>, DynamicBuffer<DamageThisFrame>>().WithAll<PlayerTag>().WithEntityAccess())
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

                if (!Mathf.Approximately(previousHealth, currentHealth.ValueRO.Value))
                {
                    PlayerGameObject.Instance.hurtAudioSource.Play();
                }
                
                // clear damageThisFrame buffer after process the damages/hits
                damageThisFrame.Clear();

                if (currentHealth.ValueRW.Value <= 0)
                {
                    // Enable DestroyEntityFlag
                }
            }
        }
    }
}