using UnityEngine;

namespace Script.Player.Weapon.Projectiles
{
    public class Base : MonoBehaviour
    {
        [SerializeField] private Data data;

        private void Start()
        {
            Destroy(gameObject, data.lifetime);
        }

        private void Update()
        {
            transform.Translate(Vector3.forward * (data.speed * Time.deltaTime));
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (data.hasSplashDamage)
                ApplySplashDamage();
            
            else
                ApplyDirectDamage(collision);

            Destroy(gameObject);
        }

        private void ApplyDirectDamage(Collision collision)
        {
            var target = collision.gameObject.GetComponent<IHitable>();
            target?.TakeDamage(data.damage);
        }

        private void ApplySplashDamage()
        {
            var hitColliders = Physics.OverlapSphere(transform.position, data.splashRadius);
            foreach (var hitCollider in hitColliders)
            {
                var target = hitCollider.GetComponent<IHitable>();
                target?.TakeDamage(data.damage);
            }
        }
    }
}
