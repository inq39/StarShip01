using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _enemySpeed = 3.0f;
    [SerializeField] private int _enemyScoreValue;
    private PlayerController _playerController;
    private Animator _destroyEnemyAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        if (_playerController == null)
        {
            Debug.LogError("Player is NULL.");
        }
        _destroyEnemyAnimator = GetComponent<Animator>();
        Destroy(gameObject, 4);

        

       
        if (_destroyEnemyAnimator == null)
        {
            Debug.LogError("Animator is NULL.");
        }
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
            _playerController.UpdatePlayerScore(_enemyScoreValue);
            _destroyEnemyAnimator.SetTrigger("IsEnemyDestroyed");

            _enemySpeed = 0f;
            Destroy(this.gameObject, 2.8f);
            Destroy(trigger.gameObject);
        }

        if (trigger.gameObject.CompareTag("Player"))
        {
            _enemySpeed = 0f;
            _destroyEnemyAnimator.SetTrigger("IsEnemyDestroyed");
            Destroy(this.gameObject, 2.8f);
           // PlayerController _player = trigger.transform.GetComponent<PlayerController>();

            if (_playerController != null)
            {
                _playerController.DestroyLive();
            }
        }
    }
}
