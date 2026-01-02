using UnityEngine;
using UnityEngine.AI;

namespace Ashking.OOP
{
    public class EnemyGameObject : MonoBehaviour
    {
        public NavMeshAgent navMeshAgent;
        public Animator animator;
        public AudioSource audioSource;
        public ParticleSystem hitParticles;
    }
}