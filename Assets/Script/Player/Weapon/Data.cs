using System;
using UnityEngine;

namespace Script.Player.Weapon
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Gun")]
    [Serializable]
    public class Data: ScriptableObject
    {
        public Projectiles.Base projectilePrefab; 
        public float fireRate = 1f; 
        public int damage = 10; 
    }
}