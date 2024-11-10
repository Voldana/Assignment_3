using System;
using UnityEngine;

namespace Script.Player.Weapon.Projectiles
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Projectile")]
    [Serializable]
    public class Data: ScriptableObject
    {
        public GameObject impactPrefab; 
        public int damage = 10; 
        public float speed = 20f; 
        public float lifetime = 5f; 
        public bool hasSplashDamage = false; 
        public float splashRadius = 0f;
    }
}