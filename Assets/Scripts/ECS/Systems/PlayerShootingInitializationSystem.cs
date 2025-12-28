using Ashking.Components;
using AshKing.OOP;
using Unity.Entities;

namespace Ashking.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct PlayerShootingInitializationSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<InitializePlayerShootingTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            if(PlayerGameObject.Instance == null)
                return;
            
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
            foreach (var (playerShootingEffectsData, entity) in SystemAPI.Query<RefRW<PlayerShootingEffectsData>>()
                         .WithAll<InitializePlayerShootingTag, PlayerTag>().WithEntityAccess())
            {
                playerShootingEffectsData.ValueRW.GunTipTransform = PlayerGameObject.Instance.gunTipTransform;
                playerShootingEffectsData.ValueRW.GunParticles = PlayerGameObject.Instance.gunParticles;
                playerShootingEffectsData.ValueRW.GunLine = PlayerGameObject.Instance.gunLine;
                playerShootingEffectsData.ValueRW.GunAudio = PlayerGameObject.Instance.gunAudio;
                playerShootingEffectsData.ValueRW.GunLight = PlayerGameObject.Instance.gunLight;
                playerShootingEffectsData.ValueRW.FaceLight = PlayerGameObject.Instance.faceLight;
                playerShootingEffectsData.ValueRW.EffectsDisplayTime = PlayerGameObject.Instance.effectsDisplayTime;
                
                ecb.RemoveComponent<InitializePlayerShootingTag>(entity);
            }
            
            ecb.Playback(state.EntityManager);
        }
    }
}