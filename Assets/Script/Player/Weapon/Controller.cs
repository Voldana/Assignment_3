using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script.Player.Weapon
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private LayerMask aimLayerMask;
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private Transform barrel;
        [SerializeField] private List<Base> gunList;

        private UnityEngine.Camera mainCamera;
        private Base selectedGun;
        private int selectedGunIndex;

        private void Start()
        {
            mainCamera = UnityEngine.Camera.main;
            selectedGun = gunList.First();
            selectedGun.SetSelected(true);
        }

        private void FixedUpdate()
        {
            AimBarrel();
            SwitchWeapon();
        }

        private void SwitchWeapon()
        {
            var scroll = Input.GetAxis("Mouse ScrollWheel");
            if(scroll == 0) return;
            var value = (int)Mathf.Sign(scroll);
            selectedGunIndex += value;
            selectedGun.SetSelected(false);
            selectedGun = gunList[selectedGunIndex % gunList.Count];
            selectedGun.SetSelected(true);
        }

        private void AimBarrel()
        {
            if (!mainCamera) return;
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, aimLayerMask)) return;
            var aimDirection = hit.point - barrel.position;
            aimDirection.y = 0;
            var targetRotation = Quaternion.LookRotation(-aimDirection);
            barrel.rotation = Quaternion.Slerp(barrel.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}