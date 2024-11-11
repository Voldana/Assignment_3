using System;
using DG.Tweening;
using Script.Player;
using UnityEngine;

namespace Script.Enemies
{
    public class Tank : Base
    {
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private float fireInterval;
        [SerializeField] private Transform firePoint;
        [SerializeField] private Transform barrel;

        private const float RotationSpeed = 5f;
        private Transform player;
        private float fireTimer;

        protected override void Start()
        {
            base.Start();
            type = Type.Tank;
            player = FindObjectOfType<Controller>().transform;
        }

        private void FixedUpdate()
        {
            AimBarrel();
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
            if (player == null) return;
            var projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            projectile.SetTarget(player);
            projectile.SetDamage(damage);
        }

        private void AimBarrel()
        {
            var aimDirection = player.position - barrel.position;
            aimDirection.y = 0;
            var targetRotation = Quaternion.LookRotation(-aimDirection);
            barrel.rotation = Quaternion.Slerp(barrel.rotation, targetRotation, Time.deltaTime * RotationSpeed);
        }
    }
}