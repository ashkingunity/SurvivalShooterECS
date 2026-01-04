using Ashking.Components;
using Ashking.OOP;
using Unity.Entities;

namespace Ashking.Systems
{
    public partial struct EnemyHealthSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (currentHealth, entity) 
                     in SystemAPI.Query<CurrentHealth>().WithPresent<DestroyEntityFlag>().WithAll<EnemyTag>().WithEntityAccess())
            {
                if (currentHealth.Value <= 0)
                {
                    // To play enemy death audio
                    SystemAPI.SetComponentEnabled<PlayAudioClipOnDestroy>(entity, true);

                    if (SystemAPI.HasComponent<Score>(entity))
                    {
                        var score = SystemAPI.GetComponent<Score>(entity);
                        ScoreManager.Instance.AddScore(score.Value);
                    }

                    // Enable DestroyEntityFlag
                    SystemAPI.SetComponentEnabled<DestroyEntityFlag>(entity, true);
                }
            }
        }
    }
}