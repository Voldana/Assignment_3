using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Script.UI
{
    public class HealthBar : MonoBehaviour
    {
        [Inject] private SignalBus signalBus;
        
        [SerializeField] private float totalHealth;
        [SerializeField] private Image health;

        private float currentHealth;
        private void Start()
        {
            currentHealth = totalHealth;
            SubscribeSignals();
        }

        private void SubscribeSignals()
        {
            signalBus.Subscribe<GameEvents.OnPlayerHit>(TakeDamage);
        }

        private void TakeDamage(GameEvents.OnPlayerHit signal)
        {
            currentHealth -= signal.damage;
            if(currentHealth <= 0)
                signalBus.Fire(new GameEvents.OnPlayerDeath());
            health.fillAmount = currentHealth / totalHealth;
        }
    }
}
