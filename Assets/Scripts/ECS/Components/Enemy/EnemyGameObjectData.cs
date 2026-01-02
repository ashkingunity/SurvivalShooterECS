using Unity.Entities;
using UnityEngine;
using UnityEngine.AI;

namespace Ashking.Components
{
    public struct EnemyGameObjectData : IComponentData
    {
        public UnityObjectRef<NavMeshAgent> NavMeshAgent;
        public UnityObjectRef<Animator> Animator;
        public UnityObjectRef<AudioSource> AudioSource;
        public UnityObjectRef<ParticleSystem> HitParticles;
    }
}
