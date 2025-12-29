using Ashking.Components;
using AshKing.Groups;
using AshKing.OOP;
using Unity.Entities;

namespace Ashking.Systems
{
    [UpdateInGroup(typeof(PlayerShootingGroup), OrderLast =  true)]
    public partial struct PlayerDisableShootingEffectSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (playerShootingEffectsData, playerShootingTimer, playerShootingData)
                     in SystemAPI.Query<RefRW<PlayerShootingEffectsData>, PlayerShootingTimer, PlayerShootingData>().WithNone<InitializePlayerShootingTag>())
            {
                if (playerShootingTimer.Value >= playerShootingEffectsData.ValueRO.EffectsDisplayTime *
                    playerShootingData.TimeBetweenShots)
                {
                    // Disable the line renderer and the light.
                    playerShootingEffectsData.ValueRW.GunLine.Value.enabled = false;
                    playerShootingEffectsData.ValueRW.FaceLight.Value.enabled = false;
                    playerShootingEffectsData.ValueRW.GunLight.Value.enabled = false;
                }
            }
        }
    }
}