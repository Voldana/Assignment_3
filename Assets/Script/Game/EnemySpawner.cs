using Script.Enemies;
using UnityEngine;
using Zenject;

namespace Script.Game
{
    public class EnemySpawner: MonoBehaviour
    {
        [Inject] private SignalBus signalBus;
        
        [SerializeField] private float minSpawnDistance = 5f;
        [SerializeField] private float spawnRadius = 20f;
        [SerializeField] private float spawnRate = .2f; 
        [SerializeField] private Base[] enemyPrefabs; 
        [SerializeField] private Transform player;

        
        private float spawnTimer;
        private int enemyTiers = 1; 
        private int minePacks = 1;

        private void Update()
        {
            spawnTimer += Time.deltaTime;
            if (!(spawnTimer >= 1f / spawnRate)) return;
            SpawnEnemy();
            spawnTimer = 0f;
        }

        public void SetSpawnRate(float rate)
        {
            spawnRate = rate;
        }

        public void SetEnemyTiers(int tiers)
        {
            enemyTiers = tiers;
        }

        public void SetMinePacks(int packs)
        {
            minePacks = packs;
        }

        private void SpawnEnemy()
        {
            var enemyType = Random.Range(0, enemyTiers); 
            var spawnPosition = GetRandomPositionAroundPlayer();
            
            if (enemyPrefabs[enemyType]){}
                Instantiate(enemyPrefabs[enemyType], spawnPosition, Quaternion.identity).SetSignalBus(signalBus);
            

            if (enemyType == 2)
                SpawnMinePack(spawnPosition);
            
            
        }

        private Vector3 GetRandomPositionAroundPlayer()
        {
            var randomDirection = Random.insideUnitSphere.normalized; 
            randomDirection.y = 0;
            var randomDistance = Random.Range(minSpawnDistance, spawnRadius);
            return player.position + randomDirection * randomDistance;
        }
        private void SpawnMinePack(Vector3 baseSpawnPoint)
        {
            for (var i = 0; i < minePacks; i++)
            {
                var randomOffset = new Vector3(
                    Random.Range(-5f, 5f),
                    0,
                    Random.Range(-5f, 5f)
                );

                Instantiate(enemyPrefabs[2], baseSpawnPoint + randomOffset, Quaternion.identity).SetSignalBus(signalBus);
            }
        }
    }
}