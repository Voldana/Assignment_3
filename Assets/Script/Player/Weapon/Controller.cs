using System.Collections.Generic;
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

        private void Start()
        {
            mainCamera = UnityEngine.Camera.main;
        }

        private void FixedUpdate()
        {
            AimBarrel();
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
