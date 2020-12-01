using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    private int _delayTime = 2;
    private int _repeatTime = 2;
    private float _spawnPositionMinX = -8.0f;
    private float _spawnPositionMaxX = 8.0f;
    private float _spawnPositionY = 7.0f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemies", _delayTime, _repeatTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemies()
    {
        Instantiate(enemyPrefab, CalculateRandomSpawnPosition(), Quaternion.identity);
    }

    Vector3 CalculateRandomSpawnPosition()
    {
        float _spawnPositionX = Random.Range(_spawnPositionMinX, _spawnPositionMaxX);
        return new Vector3(_spawnPositionX, _spawnPositionY, 0);
    }
}
