using System;
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

        void OnEnable()
        {
            GameUIController.Instance.PlayerDeadEvent += DisableMovement;
        }

        void OnDisable()
        {
            GameUIController.Instance.PlayerDeadEvent -= DisableMovement;
        }

        void DisableMovement()
        {
            navMeshAgent.enabled = false;
            animator.SetBool(SurvivalShooterAnimationHashes.WalkingHash, false);
        }
        
        public void ConfigureEnemyGameObject()
        {
            navMeshAgent.enabled = true;
        }
        
    }
}