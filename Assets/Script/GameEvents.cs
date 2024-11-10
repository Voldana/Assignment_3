using UnityEngine;

namespace Script
{
    public class GameEvents
    {
        public struct OnPlayerHit
        {
        }
        
        public struct OnShotFired
        {
            public float cooldown;
        }
        
        public struct OnGunSwitch
        {
            public float cooldown;
        }
    }
}
