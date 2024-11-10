using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Script.Player.Weapon
{
    public class Controller : MonoBehaviour
    {
        [Inject] private SignalBus signalBus;

        [SerializeField] private LayerMask aimLayerMask;
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private Transform barrel;
        [SerializeField] private List<Base> gunList;

        private UnityEngine.Camera mainCamera;
        private Base selectedGun;
        private int selectedGunIndex;
        private Vector3 target;

        private void Start()
        {
            mainCamera = UnityEngine.Camera.main;
            selectedGun = gunList.First();
            selectedGun.SetSelected(true);
            DOVirtual.DelayedCall(.5f, () =>
            {
                signalBus.Fire(new GameEvents.OnGunSwitch
                    { cooldown = selectedGun.GetData().fireRate, name = selectedGun.GetData().name });
            });
        }

        private void FixedUpdate()
        {
            AimBarrel();
            FireWeapon();
            SwitchWeapon();
        }

        private void FireWeapon()
        {
            if (Input.GetMouseButton(0))
                selectedGun.Shoot(target);
        }

        private void SwitchWeapon()
        {
            var scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll == 0) return;
            var value = (int)Mathf.Sign(scroll);
            selectedGunIndex += value;
            selectedGun.SetSelected(false);
            selectedGun = gunList[Mathf.Abs(selectedGunIndex % gunList.Count)];
            selectedGun.SetSelected(true);
            signalBus.Fire(new GameEvents.OnGunSwitch
                { cooldown = selectedGun.GetData().fireRate, name = selectedGun.GetData().name });
        }

        private void AimBarrel()
        {
            if (!mainCamera) return;
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, aimLayerMask)) return;
            target = hit.point;
            var aimDirection = hit.point - barrel.position;
            aimDirection.y = 0;
            var targetRotation = Quaternion.LookRotation(-aimDirection);
            barrel.rotation = Quaternion.Slerp(barrel.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}