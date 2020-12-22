using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _enemySpeed = 3.0f;
    [SerializeField] private int _enemyScoreValue;
    private PlayerController _playerController;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 4);
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveEnemy();
        
    }

    void MoveEnemy()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _enemySpeed);
    }
    

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Laser_Projectile"))
        {
            Destroy(this.gameObject);
            _playerController.UpdatePlayerScore(_enemyScoreValue);
            Destroy(trigger.gameObject);
        }

        if (trigger.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
           // PlayerController _player = trigger.transform.GetComponent<PlayerController>();

            if (_playerController != null)
            {
                _playerController.DestroyLive();

            }
        }
    }
}
