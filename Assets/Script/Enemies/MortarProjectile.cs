using System;
using Script.Player.Weapon;
using UnityEngine;

namespace Script.Enemies
{
    public class MortarProjectile : MonoBehaviour
    {
        [SerializeField] private GameObject sprite;
        
        private const float Range = 5;
        private float damage;

        public void SetDamage(float dmg)
        {
            damage = dmg;
            sprite.transform.SetParent(null);
        }

        private void OnTriggerEnter(Collider other)
        {
            ApplySplashDamage();
        }

        private void FixedUpdate()
        {
            if (!(transform.position.y < 0)) return;
            ApplySplashDamage();
            DestroyProjectile();
        }

        private void ApplySplashDamage()
        {
            var hitColliders = Physics.OverlapSphere(transform.position, Range);
            foreach (var hitCollider in hitColliders)
            {
                var target = hitCollider.GetComponent<IHitable>();
                target?.TakeDamage(damage);
            }
            DestroyProjectile();
        }
        
        private void DestroyProjectile()
        {
            Destroy(gameObject);
            Destroy(sprite);
        }
    }
}