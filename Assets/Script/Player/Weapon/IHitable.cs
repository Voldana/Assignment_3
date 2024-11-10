using UnityEngine;

namespace Script.Player.Weapon
{
    public interface IHitable
    { 
        public void TakeDamage(int damage);
    }
}
