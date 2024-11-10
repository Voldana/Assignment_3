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
        [SerializeField] private float baseDamage;

        private UnityEngine.Camera mainCamera;
        private float currentHealth;
        private bool buffed;
        
        protected float damage;
        protected Type type;
        
        protected virtual void Start()
        {
            mainCamera = UnityEngine.Camera.main;
            currentHealth = totalHealth;
            damage = baseDamage;
        }

        private void Update()
        {
            HealthBarLookAt();
        }

        private void HealthBarLookAt()
        {
            healthBar.transform.LookAt(mainCamera.transform);
        }

        public void TakeDamage(float damage)
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

        private void OnTriggerEnter(Collider other)
        {
            if(!other.gameObject.tag.Equals("Aura") || buffed) return;
            buffed = true;
            damage *= 2;
        }

        private void OnTriggerExit(Collider other)
        {
            if(!other.gameObject.tag.Equals("Aura")) return;
            buffed = false;
            damage = baseDamage;
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