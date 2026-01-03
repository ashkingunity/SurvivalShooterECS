using Unity.Entities;
using UnityEngine;
using Ashking.Components;

namespace Ashking.Authoring
{
    public class PlayAudioClipOnDestroyAuthoring : MonoBehaviour
    {
        public AudioClip audioClip;
        
        private class PlayAudioClipOnDestroyAuthoringBaker : Baker<PlayAudioClipOnDestroyAuthoring>
        {
            public override void Bake(PlayAudioClipOnDestroyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new PlayAudioClipOnDestroy
                {
                    AudioClip = authoring.audioClip
                });
                SetComponentEnabled<PlayAudioClipOnDestroy>(entity, false);
            }
        }
    }
}