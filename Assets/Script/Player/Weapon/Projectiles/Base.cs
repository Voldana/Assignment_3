using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Script.Player.Weapon.Projectiles
{
    public class Base : MonoBehaviour
    {
        [SerializeField] protected Data data;

        private bool destroyed;
        private Vector3 targetPos;
        protected TweenerCore<Vector3, Vector3,VectorOptions> tween;

        private void Start()
        {
            DOVirtual.DelayedCall(data.lifetime, DestroyProjectile);
        }

        public virtual void SetTarget(Vector3 target)
        {
            targetPos = target;
            tween = transform.DOMove(targetPos, data.speed).SetSpeedBased(true);
        }


        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("Gas") || other.gameObject.tag.Equals("Aura")) return;
            if (data.hasSplashDamage)
                ApplySplashDamage();
            
            else
                ApplyDirectDamage(other.gameObject);
        }

        private void ApplyDirectDamage(GameObject collision)
        {
            var target = collision.gameObject.GetComponent<IHitable>();
            target?.TakeDamage(data.damage);
            DestroyProjectile();
        }

        private void ApplySplashDamage()
        {
            var hitColliders = Physics.OverlapSphere(transform.position, data.splashRadius);
            foreach (var hitCollider in hitColliders)
            {
                var target = hitCollider.GetComponent<IHitable>();
                target?.TakeDamage(data.damage);
            }
            DestroyProjectile();
        }

        private void DestroyProjectile()
        {
            if(destroyed) return;
            tween.Kill();
            destroyed = true;
            Destroy(gameObject);
        }
    }
}
