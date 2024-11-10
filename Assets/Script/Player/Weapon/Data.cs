using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Player.Weapon
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Gun")]
    [Serializable]
    public class Data: ScriptableObject
    {
        public Projectiles.Base projectilePrefab; 
        public float fireRate = 1f; 
        [FormerlySerializedAs("name")] public string gunName; 
    }
}