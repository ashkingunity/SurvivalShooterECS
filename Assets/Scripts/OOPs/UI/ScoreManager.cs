using System;
using OOPs.Utlities;

namespace Ashking.OOP
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        public int score;

        public event Action<int> ScoreUpdatedEvent;

        public void AddScore(int value)
        {
            score += value;
            ScoreUpdatedEvent?.Invoke(score);
        }
    }
}