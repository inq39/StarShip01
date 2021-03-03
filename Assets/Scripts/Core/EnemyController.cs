﻿using StarShip01.Manager;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StarShip01.Core
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float _enemySpeed = 3.0f;
        [SerializeField] private int _enemyScoreValue;
        [SerializeField] private GameObject _enemyLaserPrefab;
        [SerializeField] private float _standardDestroyTime; // 6f
        private PlayerController _playerController;
        private Animator _destroyEnemyAnimator;
        private AudioSource _explosionSound;
        private float _speedFactor;

        void Start()
        {
            _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
            if (_playerController == null)
            {
                Debug.LogError("Player is NULL.");
            }

            _destroyEnemyAnimator = GetComponent<Animator>();
            if (_destroyEnemyAnimator == null)
            {
                Debug.LogError("Animator is NULL.");
            }

            _explosionSound = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            _speedFactor = 1.0f;
            GetComponent<Collider2D>().enabled = true;
            _destroyEnemyAnimator = GetComponent<Animator>();
            if (_destroyEnemyAnimator == null)
            {
                Debug.LogError("Animator is NULL.");
            }
            InvokeRepeating("ShootLaser", 1f, Random.Range(0, 3));
            Invoke("SetInactive", _standardDestroyTime);
        }

        private void OnDisable()
        {
            CancelInvoke();
        }

        void FixedUpdate()
        {
            MoveEnemy();
        }

        private void ShootLaser()
        {
            GameObject firedLaser = Instantiate(_enemyLaserPrefab, transform.position, Quaternion.identity);            
            Destroy(firedLaser, _standardDestroyTime);
        }

        void MoveEnemy()
        {
            transform.Translate(Vector3.down * Time.deltaTime * _enemySpeed * _speedFactor);
        }

        private void OnTriggerEnter2D(Collider2D trigger)
        {
            if (trigger.gameObject.CompareTag("Laser_Projectile")) // hit by player_laser with score
            {
                GameManager.Instance.UpdateScore(_enemyScoreValue);
                Destroy(trigger.gameObject);
                DestroyEnemy();
            }

            if (trigger.gameObject.CompareTag("Player")) // collision with player without score
            {
                DestroyEnemy();

                if (_playerController != null)
                {
                    _playerController.DestroyLive();
                }
            }
        }

        private void DestroyEnemy()
        {
            GetComponent<Collider2D>().enabled = false;
            _destroyEnemyAnimator.SetTrigger("IsEnemyDestroyed");
            _speedFactor = 0.2f;
            _explosionSound.Play();
            CancelInvoke();
            Invoke("SetInactive", 2.8f);
        }

        private void SetInactive()
        {
            _destroyEnemyAnimator.ResetTrigger("IsEnemyDestroyed");
            this.gameObject.SetActive(false);
        }
    }
}
