using UnityEngine;

namespace Script.Player.Weapon
{
    public abstract class Base : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Transform firePoint; 
        [SerializeField] private Material defaultMat;
        [SerializeField] private Material selected;
        [SerializeField] private Data data;

        private float nextFireTime;

        protected bool CanFire()
        {
            return Time.time >= nextFireTime;
        }

        protected void Fire()
        {
            if (!CanFire()) return;

            nextFireTime = Time.time + 1f / data.fireRate;
            var projectile = Instantiate(data.projectilePrefab, firePoint.position, firePoint.rotation);
            var rb = projectile.GetComponent<Rigidbody>();

            if (rb != null)
                rb.velocity = firePoint.forward * data.projectileSpeed;

            Destroy(projectile, 5f); // Destroy after 5 seconds to save memory
        }

        public void SetSelected(bool state)
        {
            meshRenderer.material = state ? selected : defaultMat;
        }

        public abstract void Shoot(); 
    }
}