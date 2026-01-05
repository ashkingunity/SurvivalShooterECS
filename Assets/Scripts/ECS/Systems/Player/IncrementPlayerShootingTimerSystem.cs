using Ashking.Components;
using Ashking.Groups;
using Unity.Entities;

namespace Ashking.Systems
{
    [UpdateInGroup(typeof(PlayerShootingGroup), OrderFirst =  true)]
    public partial struct IncrementPlayerShootingTimerSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            if (SystemAPI.HasComponent<ShootingTimer>(playerEntity))
            {
                var timerOverride = SystemAPI.GetComponentRW<ShootingTimer>(playerEntity);
                timerOverride.ValueRW.Value += SystemAPI.Time.DeltaTime;
            }
        }
    }
}