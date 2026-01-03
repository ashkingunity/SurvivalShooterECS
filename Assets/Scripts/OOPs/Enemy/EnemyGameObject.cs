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
            // // Rebind to reset the internal state and parameters to defaults
            // animator.Rebind();
            // // Force an immediate update to ensure the state is applied instantly
            // animator.Update(0f);
        }
    }
}