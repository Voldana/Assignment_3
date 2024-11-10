using System;
using TMPro;
using UnityEngine;
using Zenject;
using Type = Script.Enemies.Type;

namespace Script.UI
{
    public class HUD: MonoBehaviour
    {
        [Inject] private SignalBus signalBus;
        
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text clock;
        
        private Timer timer;
        private int score;

        private void Start()
        {
            SubscribeSignals();
            timer = new Timer(0, UpdateTime);
            timer.Start();
        }

        private void SubscribeSignals()
        {
            signalBus.Subscribe<GameEvents.OnEnemyDestroyed>(EnemyDestroyed);
        }

        private void EnemyDestroyed(GameEvents.OnEnemyDestroyed signal)
        {
            switch (signal.type)
            {
                case Type.Mine:
                    score += 10;
                    break;
                case Type.Tank:
                    score += 20;
                    break;
                case Type.Tower:
                    score += 35;
                    break;
                case Type.Mortar:
                    score += 25;
                    break;
            }
            scoreText.text = score.ToString();
        }

        private void AddScoreOnTime()
        {
            score += 5;
            scoreText.text = score.ToString();
        }

        private void UpdateTime(int newTime)
        {
            clock.text = $"{newTime / 60}:{newTime % 60:D2}";
            AddScoreOnTime();
        }
    }
}