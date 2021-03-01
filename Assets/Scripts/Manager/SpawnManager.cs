using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarShip01.Manager
{
    public class SpawnManager : MonoSingleton<SpawnManager>
    {
        [SerializeField] private GameObject _enemyPrefab; //more enemies with Array like PowerUps
        [SerializeField] private GameObject _enemyContainer;
        [SerializeField] private GameObject[] _powerUpPrefab;
        [SerializeField] private GameObject _powerUpContainer;
        [SerializeField] private GameObject _laserContainer;

        private float _spawnPositionMaxX = 8.0f;
        private float _spawnPositionY = 7.0f;

        [SerializeField] private int _minSecondsEnemiesSpawn = 2;
        [SerializeField] private int _maxSecondsEnemiesSpawn = 4;
        [SerializeField] private int _minSecondsPowerUpsSpawn = 3;
        [SerializeField] private int _maxSecondsPowerUpsSpawn = 7;

        IEnumerator SpawnEnemies()
        {
            while (GameManager.Instance.IsGameOver == false)
            {
                yield return new WaitForSeconds(Random.Range(_minSecondsEnemiesSpawn, _maxSecondsEnemiesSpawn));
                GameObject _newEnemy = Instantiate(_enemyPrefab, CalculateRandomSpawnPosition(), Quaternion.identity);
                _newEnemy.transform.parent = _enemyContainer.transform;
            }
        }

        IEnumerator SpawnPowerUps()
        {
            while (GameManager.Instance.IsGameOver == false)
            {
                yield return new WaitForSeconds(Random.Range(_minSecondsPowerUpsSpawn, _maxSecondsPowerUpsSpawn));

                int randomPrefab = Random.Range(0, _powerUpPrefab.Length);

                GameObject _newPowerUp = Instantiate(_powerUpPrefab[randomPrefab], CalculateRandomSpawnPosition(), Quaternion.identity);
                _newPowerUp.transform.parent = _powerUpContainer.transform;
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

        public void DeactivateContainer()
        {
            _enemyContainer.SetActive(false);
            _powerUpContainer.SetActive(false);
            _laserContainer.SetActive(false);
        }
        public void ActivateContainer()
        {
            _enemyContainer.SetActive(true);
            _powerUpContainer.SetActive(true);
            _laserContainer.SetActive(true);
        }
    }
}
