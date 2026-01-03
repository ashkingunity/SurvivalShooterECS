using Unity.Entities;
using UnityEngine;

namespace Ashking.Components
{
    public struct PlayAudioClipOnDestroy : IComponentData, IEnableableComponent
    {
        public UnityObjectRef<AudioClip> AudioClip;
    }
}