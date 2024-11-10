using UnityEngine;

namespace Script.Player
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed = 0.3f;
        [SerializeField] private float movementSpeed = 10f;
        
        private Quaternion targetRotation = Quaternion.identity;
        private Quaternion aimRotation = Quaternion.identity;
        private float isoCameraAngle;
        private Rigidbody rb;
        private void Start()
        {
            isoCameraAngle = Camera.main ? Camera.main.transform.parent.transform.eulerAngles.y : 45f;
            rb = GetComponent<Rigidbody>();
        }
        
        

    }
}
