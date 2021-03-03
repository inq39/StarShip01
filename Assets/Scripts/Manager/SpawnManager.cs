using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarShip01.Manager
{
    public class SpawnManager : MonoSingleton<SpawnManager>
    {
        [Header("Enemies")]
        [SerializeField] private GameObject[] _enemyPrefab; 
        [SerializeField] private int _enemyPoolSize;
        [SerializeField] private List<GameObject> _enemyPool;
        [SerializeField] private GameObject _enemyContainer;
        [Header("Power Ups")]
        [SerializeField] private GameObject[] _powerUpPrefab;
        [SerializeField] private int _powerUpPoolSize;
        [SerializeField] private List<GameObject> _powerUpPool;
        [SerializeField] private GameObject _powerUpContainer;

        private float _spawnPositionMaxX = 8.0f;
        private float _spawnPositionY = 7.0f;

        [SerializeField] private int _minSecondsEnemiesSpawn = 2;
        [SerializeField] private int _maxSecondsEnemiesSpawn = 4;
        [SerializeField] private int _minSecondsPowerUpsSpawn = 3;
        [SerializeField] private int _maxSecondsPowerUpsSpawn = 7;

        private void Start()
        {
            _enemyPool = GenerateEnemyPool();
            _powerUpPool = GeneratePowerUpPool();
        }

        private List<GameObject> GeneratePowerUpPool()
        {
            for (int i = 0; i < _powerUpPoolSize; i++ )
            {
                int randomPrefab = Random.Range(0, _powerUpPrefab.Length);
                GameObject newPowerUp = Instantiate(_powerUpPrefab[randomPrefab]);
                newPowerUp.transform.parent = _powerUpContainer.transform;
                newPowerUp.SetActive(false);
                _powerUpPool.Add(newPowerUp);
            }
            return _powerUpPool;
        }

        private List<GameObject> GenerateEnemyPool()
        {
            for (int i = 0; i < _enemyPoolSize; i++)
            {
                int randomEnemy = Random.Range(0, _enemyPrefab.Length);
                GameObject newEnemy = Instantiate(_enemyPrefab[randomEnemy]);
                newEnemy.transform.parent = _enemyContainer.transform;
                newEnemy.SetActive(false);
                _enemyPool.Add(newEnemy);
            }         
            return _enemyPool;
        }

        private GameObject RequestEnemy()
        {
            foreach (GameObject enemy in _enemyPool)
            {
                if (enemy.activeInHierarchy == false)
                {
                    enemy.SetActive(true);
                    return enemy;
                }
            }
            int randomEnemy = Random.Range(0, _enemyPrefab.Length);
            GameObject newEnemy = Instantiate(_enemyPrefab[randomEnemy]);
            newEnemy.transform.parent = _enemyContainer.transform;
            _enemyPool.Add(newEnemy);
            return newEnemy;
        }

        private GameObject RequestPowerUp()
        {
            foreach (GameObject powerUp in _powerUpPool)
            {
                if (powerUp.activeInHierarchy == false)
                {
                    powerUp.SetActive(true);
                    return powerUp;
                }
            }
            int randomPrefab = Random.Range(0, _powerUpPrefab.Length);
            GameObject newPowerUp = Instantiate(_powerUpPrefab[randomPrefab]);
            newPowerUp.transform.parent = _powerUpContainer.transform;
            _powerUpPool.Add(newPowerUp);
            return newPowerUp;
        }

        IEnumerator SpawnEnemies()
        {
            while (GameManager.Instance.IsGameOver == false)
            {
                yield return new WaitForSeconds(Random.Range(_minSecondsEnemiesSpawn, _maxSecondsEnemiesSpawn));
                GameObject enemy = RequestEnemy();
                enemy.transform.position = CalculateRandomSpawnPosition();
                enemy.transform.rotation = Quaternion.identity;
            }
        }

        IEnumerator SpawnPowerUps()
        {
            while (GameManager.Instance.IsGameOver == false)
            {
                yield return new WaitForSeconds(Random.Range(_minSecondsPowerUpsSpawn, _maxSecondsPowerUpsSpawn));
                GameObject powerUp = RequestPowerUp();
                powerUp.transform.position = CalculateRandomSpawnPosition();
                powerUp.transform.rotation = Quaternion.identity;
            }
        }

        public void StartSpawning()
        {
            StartCoroutine(SpawnEnemies());
            StartCoroutine(SpawnPowerUps());
        }

        Vector3 CalculateRandomSpawnPosition()
        {
            float _spawnPositionX = Random.Range(-_spawnPositionMaxX, _spawnPositionMaxX);
            return new Vector3(_spawnPositionX, _spawnPositionY, 0);
        }
    }
}
