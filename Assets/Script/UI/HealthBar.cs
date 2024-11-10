using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Script.UI
{
    public class HealthBar : MonoBehaviour
    {
        [Inject] private SignalBus signalBus;

        [SerializeField] private float totalHealth;
        [SerializeField] private Image health, heart;

        private float currentHealth;
        private Tweener heartTween;

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
            if (currentHealth <= 0)
                signalBus.Fire(new GameEvents.OnPlayerDeath());
            var fillAmount = currentHealth / totalHealth;
            health.fillAmount = Mathf.Clamp(fillAmount, 0, 1);
            if (currentHealth / totalHealth <= .25f)
                PumpHeart();
        }

        private void PumpHeart()
        {
            heartTween = heart.transform.DOPunchScale(Vector3.one * .25f, 2,2).SetLoops(-1);
        }
    }
}