using System;
using Script.Player;
using UnityEngine;

namespace Script.Game
{
    public class GasCloud : MonoBehaviour
    {
        [SerializeField] private float speedIncreaseRate = .2f;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float speedDecayRate = 1f;
        [SerializeField] private Controller player;
        
        private float currentSpeed;
        private void Start()
        {
            currentSpeed = moveSpeed;
        }

        private void Update()
        {
            FollowPlayer();
            if(!player.IsMoving())
                IncreaseSpeedOverTime();
            else
                DecreaseSpeedOverTime();
        }

        private void FollowPlayer()
        {
            var direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * (currentSpeed * Time.deltaTime);
        }

        private void DecreaseSpeedOverTime()
        {
            currentSpeed = Mathf.Lerp(currentSpeed, moveSpeed, speedDecayRate * Time.fixedDeltaTime);
        }
        
        private void IncreaseSpeedOverTime()
        {
            currentSpeed += speedIncreaseRate * Time.deltaTime;
        }
    }
}
