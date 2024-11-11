using Script.Player;
using UnityEngine;

namespace Script.Enemies
{
    public class Mortar: Base
    {
        [SerializeField] private MortarProjectile projectilePrefab;
        [SerializeField] private float fireInterval = 6;
        
        private Transform player;
        private float fireTimer;
        protected override void Start()
        {
            base.Start();
            type = Type.Mortar;
            player = FindObjectOfType<Controller>().transform;
        }
        
        private void FixedUpdate()
        {
            Fire();
        }
        
        private void Fire()
        {
            fireTimer += Time.fixedDeltaTime;
            if (!(fireTimer >= fireInterval)) return;
            ShootProjectile();
            fireTimer = 0f;
        }

        private void ShootProjectile()
        {
            if (!player) return;
            var projectile = Instantiate(projectilePrefab, player.position + Vector3.up * 25, Quaternion.identity);
            projectile.SetDamage(damage);
        }
    }
}