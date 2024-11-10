using System;
using Script.Player.Weapon;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Script.Enemies
{
    public abstract class Base: MonoBehaviour, IHitable
    {
        [Inject] private SignalBus signalBus;
        
        [SerializeField] private float totalHealth;
        [SerializeField] private Image healthBar;

        private UnityEngine.Camera mainCamera;
        private float currentHealth;
        protected Type type;
        protected virtual void Start()
        {
            currentHealth = totalHealth;
            mainCamera = UnityEngine.Camera.main;
        }

        private void Update()
        {
            HealthBarLookAt();
        }

        private void HealthBarLookAt()
        {
            healthBar.transform.LookAt(mainCamera.transform);
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                signalBus.Fire(new GameEvents.OnEnemyDestroyed{type = type});
                Destroy(gameObject);
                return;
            }
            healthBar.fillAmount = currentHealth / totalHealth;
        }
    }

    public enum Type
    {
        Tank,
        Mortar,
        Mine,
        Tower
    }
}