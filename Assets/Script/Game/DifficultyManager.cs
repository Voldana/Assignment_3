using UnityEngine;
using Zenject;

namespace Script.Game
{
    public class DifficultyManager : MonoBehaviour
    {
        [Inject] private SignalBus signalBus;
        
        public EnemySpawner enemySpawner; // Reference to the enemy spawner
        public float difficultyIncreaseInterval = 20f; // Time (in seconds) between difficulty increases
        public float initialSpawnRate = .3f; // Initial spawn rate (enemies per second)
        public float spawnRateIncrease = 0.1f; // How much to increase the spawn rate each interval
        public float maxSpawnRate = 5f; // Maximum spawn rate
        public int maxEnemyTiers = 3; // Maximum enemy tiers to unlock
        public int maxMinePacks = 5; // Maximum packs of mines

        private float currentSpawnRate;
        private float elapsedTime = 0f; // Tracks elapsed time

        private void Start()
        {
            currentSpawnRate = initialSpawnRate;
            enemySpawner.SetSpawnRate(currentSpawnRate);
        }

        private void Update()
        {
            elapsedTime += Time.deltaTime;

            if (!(elapsedTime >= difficultyIncreaseInterval)) return;
            IncreaseDifficulty();
            elapsedTime = 0f; // Reset timer
        }

        private void IncreaseDifficulty()
        {
            // Increase spawn rate
            currentSpawnRate = Mathf.Min(currentSpawnRate + spawnRateIncrease, maxSpawnRate);
            enemySpawner.SetSpawnRate(currentSpawnRate);

            // Unlock additional enemy tiers
            int currentTier = Mathf.FloorToInt((currentSpawnRate - initialSpawnRate) / spawnRateIncrease);
            int enemyTiersToUnlock = Mathf.Clamp(currentTier, 1, maxEnemyTiers);
            enemySpawner.SetEnemyTiers(enemyTiersToUnlock);

            // Adjust mine packs and spawn locations
            int minePacks = Mathf.Clamp(currentTier, 1, maxMinePacks);
            enemySpawner.SetMinePacks(minePacks);
        }
    }
}