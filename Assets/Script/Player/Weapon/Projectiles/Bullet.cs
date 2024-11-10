using DG.Tweening;
using UnityEngine;

namespace Script.Player.Weapon.Projectiles
{
    public class Bullet: Base
    {
        public override void SetTarget(Vector3 target)
        {
            tween = transform.DOMove(-transform.forward * 200, data.speed).SetSpeedBased(true);
        }
    }
}