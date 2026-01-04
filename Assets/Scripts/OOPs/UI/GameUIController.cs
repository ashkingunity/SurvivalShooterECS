using OOPs.Utlities;
using TMPro;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Ashking.OOP
{
    public class GameUIController : Singleton<GameUIController>
    {
        bool isGamePaused = false;
        
        public Slider healthSlider;
        public Image damageImage;
        public TextMeshProUGUI scoreText;

        public float flashSpeed = 5f;
        public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

        private bool damaged;

        void OnEnable()
        {
            ScoreManager.Instance.OnScoreUpdated += UpdateScore;
        }

        void OnDisable()
        {
            ScoreManager.Instance.OnScoreUpdated -= UpdateScore;
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

        void UpdateScore(int score)
        {
            scoreText.text = $"SCORE: {score}";
        }

        public void PauseGame(bool pause)
        {
            isGamePaused = pause;
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