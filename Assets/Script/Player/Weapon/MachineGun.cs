using System;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Player.Weapon
{
    public class MachineGun: Base
    {
        [SerializeField] private float overheatLimit = 5f;
        [SerializeField] private float coolDownRate = .1f;
        [SerializeField] private Image overheat;
        
        private bool isOverheated;
        private float heatLevel;
        public override void Shoot(Vector3 target)
        {
            if (isOverheated) return;
            Fire(target);
            heatLevel += Time.deltaTime;
            if (!(heatLevel >= overheatLimit)) return;
            isOverheated = true;
        }

        private void FixedUpdate()
        {
            if(heatLevel == 0) return;
            overheat.fillAmount = heatLevel / overheatLimit;
            heatLevel -= coolDownRate * Time.deltaTime * .1f;
            if (!(heatLevel <= 0)) return;
            heatLevel = 0;
            isOverheated = false;
        }
    }
}