using DG.Tweening;
using UnityEngine;

namespace Script.Player.Weapon.Projectiles
{
    public class Base : MonoBehaviour
    {
        [SerializeField] private Data data;

        private Vector3 targetPos;

        private void Start()
        {
            Destroy(gameObject, data.lifetime);
        }

        public void SetTarget(Vector3 target)
        {
            targetPos = target;
        }

        private void Update()
        {
            transform.DOMove(targetPos, data.speed).SetSpeedBased(true);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (data.hasSplashDamage)
                ApplySplashDamage();
            
            else
                ApplyDirectDamage(collision);
        }

        private void ApplyDirectDamage(Collision collision)
        {
            if(collision.gameObject.tag.Equals("Player")) return;
            var target = collision.gameObject.GetComponent<IHitable>();
            target?.TakeDamage(data.damage);
            Destroy(gameObject);
        }

        private void ApplySplashDamage()
        {
            var hitColliders = Physics.OverlapSphere(transform.position, data.splashRadius);
            if(hitColliders[0].gameObject.tag.Equals("Player")) return;
            foreach (var hitCollider in hitColliders)
            {
                var target = hitCollider.GetComponent<IHitable>();
                target?.TakeDamage(data.damage);
            }
            Destroy(gameObject);
        }
    }
}
