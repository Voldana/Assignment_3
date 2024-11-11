using System;
using Script.Player.Weapon;
using UnityEngine;
using Zenject;

namespace Script.Player
{
    public class Controller : MonoBehaviour, IHitable
    {
        [Inject] private SignalBus signalBus;
        
        [SerializeField] private float rotateSpeed = 100f; 
        [SerializeField] private float strafeSpeed = 7f;
        [SerializeField] private float moveSpeed = 10f;
        
        private bool isMoving;
        private bool isInGas;
        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Move();
            if(isInGas)
                TakeDamage(.5f);
        }

        private void Move()
        {
            var rotate = Input.GetAxis("Horizontal") * rotateSpeed * Time.fixedDeltaTime;
            var move = Input.GetAxis("Vertical") * moveSpeed * Time.fixedDeltaTime; 

            var strafe = 0f;
            if (Input.GetKey(KeyCode.Q))
                strafe = -strafeSpeed * Time.fixedDeltaTime; 
            if (Input.GetKey(KeyCode.E))
                strafe = strafeSpeed * Time.fixedDeltaTime;
            
            if (strafe == 0 && move == 0)
                isMoving = false;
            else
                isMoving = true;
            
            var movement = transform.forward * move + transform.right * strafe;
            rb.MovePosition(rb.position + movement);
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0, rotate, 0));
        }

        public bool IsMoving()
        {
            return isMoving;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.tag.Equals("Gas")) return;
            isInGas = true;
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.tag.Equals("Gas")) return;
            isInGas = false;
        }

        public void TakeDamage(float damage)
        {
            signalBus.Fire(new GameEvents.OnPlayerHit{damage = damage});
        }
    }
}
