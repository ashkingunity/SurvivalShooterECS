using Ashking.Components;
using Ashking.Groups;
using Unity.Entities;

namespace Ashking.Systems
{
    [UpdateInGroup(typeof(PlayerShootingGroup), OrderLast =  true)]
    public partial struct PlayerDisableShootingEffectSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (playerGameObjectData, playerShootingData, shootingTimer)
                     in SystemAPI.Query<RefRW<PlayerGameObjectData>, PlayerShootingData, ShootingTimer>().WithNone<InitializePlayerEntityTag>().WithAll<PlayerTag>())
            {
                if (shootingTimer.Value >= playerGameObjectData.ValueRO.EffectsDisplayTime * playerShootingData.TimeBetweenShots)
                {
                    // Disable the line renderer and the light.
                    playerGameObjectData.ValueRW.GunLine.Value.enabled = false;
                    playerGameObjectData.ValueRW.FaceLight.Value.enabled = false;
                    playerGameObjectData.ValueRW.GunLight.Value.enabled = false;
                }
            }
        }
    }
}