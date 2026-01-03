using Ashking.Components;
using Unity.Entities;

namespace Ashking.Systems
{
    public partial struct EnemyHealthSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (currentHealth, entity) 
                     in SystemAPI.Query<CurrentHealth>().WithDisabled<PlayAudioClipOnDestroy>().WithPresent<DestroyEntityFlag>().WithEntityAccess())
            {
                if (currentHealth.Value <= 0)
                {
                    // To play enemy death audio
                    SystemAPI.SetComponentEnabled<PlayAudioClipOnDestroy>(entity, true);

                    // Enable DestroyEntityFlag
                    SystemAPI.SetComponentEnabled<DestroyEntityFlag>(entity, true);
                }
            }
        }
    }
}