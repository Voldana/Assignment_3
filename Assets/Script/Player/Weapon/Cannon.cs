using UnityEngine;

namespace Script.Player.Weapon
{
    public class Cannon: Base
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