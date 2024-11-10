using UnityEngine;
using Zenject;

namespace Script.Player.Weapon
{
    public abstract class Base : MonoBehaviour
    {
        [Inject] private SignalBus signalBus;

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

        protected void Fire(Vector3 target)
        {
            if (!CanFire()) return;
            signalBus.Fire(new GameEvents.OnShotFired { cooldown = data.fireRate , name = data.gunName});
            nextFireTime = Time.time + data.fireRate;
            var projectile = Instantiate(data.projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.SetTarget(target);
        }

        public void SetSelected(bool state)
        {
            meshRenderer.material = state ? selected : defaultMat;
        }
        
        public Data GetData()
        {
            return data;
        }

        public abstract void Shoot(Vector3 target);
    }
}