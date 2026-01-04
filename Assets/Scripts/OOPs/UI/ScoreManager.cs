using System;
using OOPs.Utlities;

namespace Ashking.OOP
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        public int score;

        public event Action<int> OnScoreUpdated;

        public void AddScore(int value)
        {
            score += value;
            OnScoreUpdated?.Invoke(score);
        }
    }
}