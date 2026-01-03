using Ashking.Components;
using Ashking.Groups;
using Unity.Entities;

namespace Ashking.Systems
{
    [UpdateInGroup(typeof(EntityDestructionGroup), OrderFirst = true)]
    public partial struct PlayAudioClipOnDestroySystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (audioClip, playAudioClip, entity) 
                     in SystemAPI.Query<PlayAudioClipOnDestroy, EnabledRefRW<PlayAudioClipOnDestroy>>().WithEntityAccess())
            {
                if (SystemAPI.HasComponent<PlayerTag>(entity))
                {
                    if (SystemAPI.HasComponent<PlayerGameObjectData>(entity))
                    {
                      var playerGameObjectData=  SystemAPI.GetComponentRW<PlayerGameObjectData>(entity);
                      playerGameObjectData.ValueRW.PlayerAudioSource.Value.clip = audioClip.AudioClip;
                      playerGameObjectData.ValueRW.PlayerAudioSource.Value.Play();
                    }
                }
                if (SystemAPI.HasComponent<EnemyTag>(entity))
                {
                    if (SystemAPI.HasComponent<EnemyGameObjectData>(entity))
                    {
                        var enemyGameObjectData=  SystemAPI.GetComponentRW<EnemyGameObjectData>(entity);
                        enemyGameObjectData.ValueRW.AudioSource.Value.PlayOneShot(audioClip.AudioClip);
                        
                    }
                }
                playAudioClip.ValueRW = false;
            }
        }
    }
}