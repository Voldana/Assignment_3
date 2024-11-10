using UnityEngine;

namespace Script.Camera
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private Vector3 offset = new(0, 5, -10);
        [SerializeField] private float positionSmoothSpeed = 0.125f;
        [SerializeField] private float rotationSmoothSpeed = 5f; 
        [SerializeField] private Transform target; 

        private Vector3 velocity = Vector3.zero;

        private void FixedUpdate()
        {
            if (!target) return;
            
            var targetPosition = target.position + target.TransformDirection(offset);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, positionSmoothSpeed);
            var targetRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothSpeed * Time.deltaTime);
        }
    }
}
