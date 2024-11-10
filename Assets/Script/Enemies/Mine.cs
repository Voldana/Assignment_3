using System;
using Script.Player.Weapon;
using UnityEngine;

namespace Script.Enemies
{
    public class Mine: Base
    {
        protected override void Start()
        {
            base.Start();
            type = Type.Mine;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.tag.Equals("Player")) return;
            other.gameObject.GetComponent<IHitable>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}