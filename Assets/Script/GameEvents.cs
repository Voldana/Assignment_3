using UnityEngine;

namespace Script
{
    public class GameEvents
    {
        public struct OnPlayerHit
        {
            public int damage;
        }
        public struct OnPlayerHeal
        {
            public int health;
        }
        
        public struct OnPlayerDeath
        {
        }
        
        public struct OnShotFired
        {
            public float cooldown;
            public string name;
        }
        
        public struct OnGunSwitch
        {
            public float cooldown;
            public string name;
        }
    }
}
