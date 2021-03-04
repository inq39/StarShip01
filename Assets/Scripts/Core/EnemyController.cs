﻿using StarShip01.Manager;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StarShip01.Core
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float _enemySpeed = 3.0f;
        [SerializeField] private int _enemyScoreValue;
        [SerializeField] private float _returnToPoolTime; // 6f
        [SerializeField] private Sprite _defaultSprite;
        private Animator _destroyEnemyAnimator;
        private AudioSource _explosionSound;
        private float _speedFactor;

        void Start()
        {
            _destroyEnemyAnimator = GetComponent<Animator>();
            if (_destroyEnemyAnimator == null)           
                Debug.LogError("Animator is NULL.");
            

            _explosionSound = GetComponent<AudioSource>();
            if (_explosionSound == null)
                Debug.LogError("ExplosionSound is NULL.");
        }

        private void OnEnable()
        {
            _speedFactor = 1.0f;
            InvokeRepeating("ShootLaser", 1f, Random.Range(0, 3));
            Invoke("SetEnemyInactive", _returnToPoolTime);
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
            GameObject firedLaser = SpawnManager.Instance.RequestEnemyLaser();
            firedLaser.transform.position = transform.position;
            firedLaser.transform.rotation = Quaternion.identity;
        }

        private void SetEnemyInactive()
        {
            this.gameObject.SetActive(false);
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
                trigger.gameObject.SetActive(false);
                DestroyEnemy();
            }

            if (trigger.gameObject.CompareTag("Player")) // collision with player without score
            {
                DestroyEnemy();
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
            GetComponent<SpriteRenderer>().sprite = _defaultSprite;
            GetComponent<Collider2D>().enabled = true;
            this.gameObject.SetActive(false);
        }
    }
}