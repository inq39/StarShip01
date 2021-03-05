using StarShip01.Core;
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
        [Header("Player Laser")]
        [SerializeField] private GameObject _playerLaserPrefab;
        [SerializeField] private int _playerLaserPoolSize;
        [SerializeField] private List<GameObject> _playerLaserPool;
        [SerializeField] private GameObject _playerLaserContainer;
        [Header("Player Triple Laser")]
        [SerializeField] private GameObject _tripleLaserPrefab;
        [SerializeField] private int _tripleLaserPoolSize;
        [SerializeField] private List<GameObject> _tripleLaserPool;
        [SerializeField] private GameObject _tripleLaserContainer;
        [Header("Enemy Laser")]
        [SerializeField] private GameObject _enemyLaserPrefab;
        [SerializeField] private int _enemyLaserPoolSize;
        [SerializeField] private List<GameObject> _enemyLaserPool;
        [SerializeField] private GameObject _enemyLaserContainer;

        private float _spawnPositionMaxX = 8.0f;
        private float _spawnPositionY = 7.0f;

        [Header("Spawn Values")]
        [SerializeField] private int _minSecondsEnemiesSpawn = 2;
        [SerializeField] private int _maxSecondsEnemiesSpawn = 4;
        [SerializeField] private int _minSecondsPowerUpsSpawn = 3;
        [SerializeField] private int _maxSecondsPowerUpsSpawn = 7;

        private void Start()
        {
            GeneratePools();
        }

        public void GeneratePools()
        {
            _powerUpPool = GeneratePowerUpPool();
            _enemyPool = GenerateEnemyPool();
            _playerLaserPool = GeneratePlayerLaserPool();
            _tripleLaserPool = GenerateTripleLaserPool();
            _enemyLaserPool = GenerateEnemyLaserPool();
        }

        public void SetAllListsInactive()
        {
            //todo: implementing nested list

            foreach (var powerUp in _powerUpPool)
            {
                powerUp.SetActive(false);
            }

            foreach (var enemy in _enemyPool)
            {
                enemy.GetComponent<EnemyController>().SetSprite();
                enemy.SetActive(false);
            }

            foreach (var playerLaser in _playerLaserPool)
            {
                playerLaser.SetActive(false);
            }

            foreach (var tripleLaser in _tripleLaserPool)
            {
                tripleLaser.SetActive(false);
            }

            foreach (var enemyLaser in _enemyLaserPool)
            {
                enemyLaser.SetActive(false);
            }
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
                    enemy.GetComponent<BoxCollider2D>().enabled = true;
                    return enemy;
                }
            }
            int randomEnemy = Random.Range(0, _enemyPrefab.Length);
            GameObject newEnemy = Instantiate(_enemyPrefab[randomEnemy]);
            newEnemy.transform.parent = _enemyContainer.transform;
            _enemyPool.Add(newEnemy);
            return newEnemy;
        }

        private List<GameObject> GeneratePlayerLaserPool()
        {
            for (int i = 0; i < _playerLaserPoolSize; i++)
            {
                GameObject newPlayerLaser = Instantiate(_playerLaserPrefab);
                newPlayerLaser.transform.parent = _playerLaserContainer.transform;
                newPlayerLaser.SetActive(false);
                _playerLaserPool.Add(newPlayerLaser);
            }
            return _playerLaserPool;
        }

        public GameObject RequestPlayerLaser()
        {
            foreach (GameObject playerLaser in _playerLaserPool)
            {
                if (playerLaser.activeInHierarchy == false)
                {
                    playerLaser.SetActive(true);
                    return playerLaser;
                }
            }
            GameObject newPlayerLaser = Instantiate(_playerLaserPrefab);
            newPlayerLaser.transform.parent = _playerLaserContainer.transform;
            _playerLaserPool.Add(newPlayerLaser);
            return newPlayerLaser;
        }

        private List<GameObject> GenerateTripleLaserPool()
        {
            for (int i = 0; i < _tripleLaserPoolSize; i++)
            {
                GameObject newTripleLaser = Instantiate(_tripleLaserPrefab);
                newTripleLaser.transform.parent = _tripleLaserContainer.transform;
                newTripleLaser.SetActive(false);
                _tripleLaserPool.Add(newTripleLaser);
            }
            return _tripleLaserPool;
        }

        public GameObject RequestTripleLaser()
        {
            foreach (GameObject tripleLaser in _tripleLaserPool)
            {
                if (tripleLaser.activeInHierarchy == false)
                {
                    tripleLaser.SetActive(true);
                    return tripleLaser;
                }
            }
            GameObject newTripleLaser = Instantiate(_tripleLaserPrefab);
            newTripleLaser.transform.parent = _tripleLaserContainer.transform;
            _tripleLaserPool.Add(newTripleLaser);
            return newTripleLaser;
        }

        private List<GameObject> GenerateEnemyLaserPool()
        {
            for (int i = 0; i < _enemyLaserPoolSize; i++)
            {
                GameObject newEnemyLaser = Instantiate(_enemyLaserPrefab);
                newEnemyLaser.transform.parent = _enemyLaserContainer.transform;
                newEnemyLaser.SetActive(false);
                _enemyLaserPool.Add(newEnemyLaser);
            }
            return _enemyLaserPool;
        }

        public GameObject RequestEnemyLaser()
        {
            foreach (GameObject enemyLaser in _enemyLaserPool)
            {
                if (enemyLaser.activeInHierarchy == false)
                {
                    enemyLaser.SetActive(true);
                    return enemyLaser;
                }
            }
            GameObject newEnemyLaser = Instantiate(_enemyLaserPrefab);
            newEnemyLaser.transform.parent = _enemyLaserContainer.transform;
            _enemyLaserPool.Add(newEnemyLaser);
            return newEnemyLaser;
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
