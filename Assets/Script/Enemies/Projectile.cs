using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Script.Player.Weapon;
using UnityEngine;

namespace Script.Enemies
{
    public class Projectile : MonoBehaviour
    {
        private float damage;
        private TweenerCore<Vector3, Vector3, VectorOptions> tween;

        private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.tag.Equals("Enemy")) return;
            switch (other.gameObject.tag)
            {
                case "Player":
                    other.gameObject.GetComponent<IHitable>().TakeDamage(damage);
                    break;
            }
            DestroyProjectile();
        }

        private void DestroyProjectile()
        {
            tween.Kill();
            Destroy(gameObject);
        }

        public void SetDamage(float dmg)
        {
            damage = dmg;
        }

        public void SetTarget(Transform target)
        {
            tween = transform.DOMove(target.position, 10).SetSpeedBased(true);
        }
    }
}