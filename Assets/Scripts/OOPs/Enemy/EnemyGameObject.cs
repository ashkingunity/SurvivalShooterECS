using UnityEngine;
using UnityEngine.AI;

namespace Ashking.OOP
{
    public class EnemyGameObject : MonoBehaviour
    {
        public EnemyName enemyName;
        public NavMeshAgent navMeshAgent;
        public Animator animator;
        public AudioSource audioSource;
        public ParticleSystem hitParticles;
        
        public void ConfigureEnemyGameObject()
        {
            navMeshAgent.enabled = true;
        }
    }
}