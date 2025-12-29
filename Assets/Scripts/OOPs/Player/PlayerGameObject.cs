using OOPs.Utlities;
using UnityEngine;

namespace AshKing.OOP
{
    public class PlayerGameObject : Singleton<PlayerGameObject>
    {
        public Animator animator;
        
        [Header("Shooting References")]
        public Transform gunTipTransform;               // Reference to the gun's tip
        public ParticleSystem gunParticles;             // Reference to the particle system
        public LineRenderer gunLine;                    // Reference to the line renderer
        public AudioSource gunAudio;                    // Reference to the audio source
        public Light gunLight;                          // Reference to the light component
        public Light faceLight;							// Duh
        public float effectsDisplayTime = 0.2f;         // The proportion of the timeBetweenBullets that the effects will display for
    }
}
