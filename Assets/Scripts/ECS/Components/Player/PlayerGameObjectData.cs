using Unity.Entities;
using UnityEngine;

namespace Ashking.Components
{
    public struct PlayerGameObjectData : IComponentData
    {
        public UnityObjectRef<Animator> Animator;
        
        public UnityObjectRef<Transform> GunTipTransform;               // Reference to the gun's tip
        public UnityObjectRef<ParticleSystem> GunParticles;             // Reference to the particle system
        public UnityObjectRef<LineRenderer> GunLine;                    // Reference to the line renderer
        public UnityObjectRef<AudioSource> GunAudio;                    // Reference to the audio source
        public UnityObjectRef<Light> GunLight;                          // Reference to the light component
        public UnityObjectRef<Light> FaceLight;							// Duh
        public float EffectsDisplayTime;                                // The proportion of the timeBetweenBullets that the effects will display for
    }
}