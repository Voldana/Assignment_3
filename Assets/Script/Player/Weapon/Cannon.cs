using UnityEngine;

namespace Script.Player.Weapon
{
    public class Cannon: Base
    {
        public override void Shoot()
        {
            Fire();
            if(!CanFire()) return;
            PlayParticle();
        }

        private void PlayParticle()
        {
            
        }
    }
}