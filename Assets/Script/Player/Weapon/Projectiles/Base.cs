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
        private TweenerCore<Vector3, Vector3,VectorOptions> tween;

        private void Start()
        {
            DOVirtual.DelayedCall(data.lifetime, DestroyProjectile);
        }

        public void SetTarget(Vector3 target)
        {
            targetPos = target;
            tween = transform.DOMove(targetPos, data.speed).SetSpeedBased(true);
        }


        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag.Equals("Player")) return;
            if (data.hasSplashDamage)
                ApplySplashDamage();
            
            else
                ApplyDirectDamage(collision);
        }

        private void ApplyDirectDamage(Collision collision)
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
