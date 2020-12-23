using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject[] _powerUpPrefab;
    [SerializeField] private GameObject _powerUpContainer;
    [SerializeField] private GameObject _laserContainer;
    private bool _gameOver = false;
    private float _spawnPositionMaxX = 8.0f;
    private float _spawnPositionY = 7.0f;


    private void Update()
    {
      if (_gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (!_gameOver) 
        { 
        yield return new WaitForSeconds(Random.Range(2, 4));
        GameObject _newEnemy = Instantiate(_enemyPrefab, CalculateRandomSpawnPosition(), Quaternion.identity);
            _newEnemy.transform.parent = _enemyContainer.transform;
        }
    }

    IEnumerator SpawnPowerUps()
    {
        while (!_gameOver)
        {
            yield return new WaitForSeconds(Random.Range(3, 7));
                        
            int randomPrefab = Random.Range(0, _powerUpPrefab.Length);

            GameObject _newPowerUp = Instantiate(_powerUpPrefab[randomPrefab], CalculateRandomSpawnPosition(), Quaternion.identity);
            _newPowerUp.transform.parent = _powerUpContainer.transform;        
        }

    }


    Vector3 CalculateRandomSpawnPosition()
    {
        float _spawnPositionX = Random.Range(-_spawnPositionMaxX, _spawnPositionMaxX);
        return new Vector3(_spawnPositionX, _spawnPositionY, 0);
    }

    public void StartGame()
    {
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnPowerUps());
    }

    public void GameOver()
    {
        _gameOver = true;
        _enemyContainer.SetActive(false);
        _powerUpContainer.SetActive(false);
        _laserContainer.SetActive(false);
    }

}
