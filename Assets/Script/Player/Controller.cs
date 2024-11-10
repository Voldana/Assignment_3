using UnityEngine;
namespace Script.Player
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed = 100f; 
        [SerializeField] private float strafeSpeed = 7f;
        [SerializeField] private float moveSpeed = 10f;
        
        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            var rotate = Input.GetAxis("Horizontal") * rotateSpeed * Time.fixedDeltaTime;
            var move = Input.GetAxis("Vertical") * moveSpeed * Time.fixedDeltaTime; 

            var strafe = 0f;
            if (Input.GetKey(KeyCode.Q))
                strafe = -strafeSpeed * Time.fixedDeltaTime; 
            if (Input.GetKey(KeyCode.E))
                strafe = strafeSpeed * Time.fixedDeltaTime; 

            var movement = transform.forward * move + transform.right * strafe;
            rb.MovePosition(rb.position + movement);
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0, rotate, 0));
        }
    }
}
