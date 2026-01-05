using System;
using OOPs.Utlities;
using TMPro;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Ashking.OOP
{
    [RequireComponent(typeof(ScoreManager))]
    public class GameUIController : Singleton<GameUIController>
    {
        public Slider healthSlider;
        public Image damageImage;
        public TextMeshProUGUI scoreText;

        public float flashSpeed = 5f;
        public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

        bool damaged;
        
        public event Action PlayerDeadEvent; 

        void Start()
        {
            ScoreManager.Instance.ScoreUpdatedEvent += OnScoreUpdated;
        }

        void OnDisable()
        {
            ScoreManager.Instance.ScoreUpdatedEvent -= OnScoreUpdated;
        }

        void Update()
        {
            damageImage.color = damaged ? flashColor : Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);

            damaged = false;
        }

        public void OnPlayerTookDamage(float newHealth)
        {
            healthSlider.value = newHealth;
            damaged = true;
        }

        public void OnPlayerDead()
        {
            PlayerDeadEvent?.Invoke();
            PauseGame(true);
        }

        void OnScoreUpdated(int score)
        {
            scoreText.text = $"SCORE: {score}";
        }

        void PauseGame(bool pause)
        {
            SetEcsEnabled(!pause);
        }
        
        void SetEcsEnabled(bool shouldEnable)
        {
            var defaultWorld = World.DefaultGameObjectInjectionWorld;
            if (defaultWorld == null) return;
            var initializationSystemGroup = defaultWorld.GetExistingSystemManaged<InitializationSystemGroup>();
            initializationSystemGroup.Enabled = shouldEnable;
            
            var simulationSystemGroup = defaultWorld.GetExistingSystemManaged<SimulationSystemGroup>();
            simulationSystemGroup.Enabled = shouldEnable;
        }
    }
}