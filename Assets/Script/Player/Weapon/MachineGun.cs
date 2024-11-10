using UnityEngine;

namespace Script.Player.Weapon
{
    public class MachineGun: Base
    {
        [SerializeField] private float overheatLimit = 5f;
        [SerializeField] private float coolDownRate = 1f; 
        private bool isOverheated;
        private float heatLevel;
        public override void Shoot(Vector3 target)
        {
            if (!isOverheated) 
            {
                Fire(target);
                heatLevel += Time.deltaTime;
                if (!(heatLevel >= overheatLimit)) return;
                isOverheated = true;
            }
            else
            {
                heatLevel -= coolDownRate * Time.deltaTime;
                if (!(heatLevel <= 0)) return;
                heatLevel = 0;
                isOverheated = false;
            }
        }
    }
}