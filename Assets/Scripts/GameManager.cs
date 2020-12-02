using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _gameOver = false;
    private float _spawnPositionMaxX = 8.0f;
    private float _spawnPositionY = 7.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemies()
    {
        while (_gameOver == false) 
        { 
        yield return new WaitForSeconds(Random.Range(2, 4));
        GameObject _newEnemy = Instantiate(_enemyPrefab, CalculateRandomSpawnPosition(), Quaternion.identity);
            _newEnemy.transform.parent = _enemyContainer.transform;
        }
    }


    Vector3 CalculateRandomSpawnPosition()
    {
        float _spawnPositionX = Random.Range(-_spawnPositionMaxX, _spawnPositionMaxX);
        return new Vector3(_spawnPositionX, _spawnPositionY, 0);
    }

    public void GameOver()
    {
        _gameOver = true;
        _enemyContainer.SetActive(false);
    }

}
