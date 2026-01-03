using Ashking.Components;
using Unity.Entities;

namespace Ashking.Systems
{
    public partial struct EnemyHealthSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (currentHealth, entity) in SystemAPI.Query<CurrentHealth>().WithPresent<DestroyEntityFlag>().WithAll<EnemyTag>().WithEntityAccess())
            {
                if (currentHealth.Value <= 0)
                {
                    // Enable DestroyEntityFlag
                    SystemAPI.SetComponentEnabled<DestroyEntityFlag>(entity, true);
                }
            }
        }
        
    }
}