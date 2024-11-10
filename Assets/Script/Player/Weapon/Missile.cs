namespace Script.Player.Weapon
{
    public class Missile: Base
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