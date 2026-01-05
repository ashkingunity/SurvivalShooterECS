using Ashking.Components;
using AshKing.OOP;
using Unity.Entities;

namespace Ashking.Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct PlayerGameObjectDataInitializationSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<InitializePlayerEntityTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            if(PlayerGameObject.Instance == null)
                return;
            
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
            foreach (var (playerGameObjectData, entity) in SystemAPI.Query<RefRW<PlayerGameObjectData>>()
                         .WithAll<InitializePlayerEntityTag, PlayerTag>().WithEntityAccess())
            {
                // Initialize animator
                playerGameObjectData.ValueRW.Animator = PlayerGameObject.Instance.animator;
                
                // Initialize player audio
                playerGameObjectData.ValueRW.PlayerAudioSource = PlayerGameObject.Instance.playerAudioSource;
                
                // Initialize shooting effects
                playerGameObjectData.ValueRW.GunTipTransform = PlayerGameObject.Instance.gunTipTransform;
                playerGameObjectData.ValueRW.GunParticles = PlayerGameObject.Instance.gunParticles;
                playerGameObjectData.ValueRW.GunLine = PlayerGameObject.Instance.gunLine;
                playerGameObjectData.ValueRW.GunAudio = PlayerGameObject.Instance.gunAudio;
                playerGameObjectData.ValueRW.GunLight = PlayerGameObject.Instance.gunLight;
                playerGameObjectData.ValueRW.FaceLight = PlayerGameObject.Instance.faceLight;
                playerGameObjectData.ValueRW.EffectsDisplayTime = PlayerGameObject.Instance.effectsDisplayTime;
                
                ecb.RemoveComponent<InitializePlayerEntityTag>(entity);
            }
            
            ecb.Playback(state.EntityManager);
        }
    }
}