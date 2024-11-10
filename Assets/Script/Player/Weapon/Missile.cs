using UnityEngine;

namespace Script.Player.Weapon
{
    public class Missile: Base
    {
        public override void Shoot(Vector3 target)
        {
            Fire(target);
            if(!CanFire()) return;
            PlayParticle();
        }
        
        private void PlayParticle()
        {
            
        }
    }
}