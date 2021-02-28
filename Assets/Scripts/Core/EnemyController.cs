using StarShip01.Manager;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StarShip01.Core
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float _enemySpeed = 3.0f;
        [SerializeField] private int _enemyScoreValue;
        [SerializeField] private GameObject _enemyLaserPrefab;
        private PlayerController _playerController;
        private Animator _destroyEnemyAnimator;
        private AudioSource _explosionSound;


        void Start()
        {
            _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
            if (_playerController == null)
            {
                Debug.LogError("Player is NULL.");
            }
            _destroyEnemyAnimator = GetComponent<Animator>();
            _explosionSound = GetComponent<AudioSource>();

            InvokeRepeating("ShootLaser", 1f, Random.Range(0, 3));
            Destroy(gameObject, 6);

            if (_destroyEnemyAnimator == null)
            {
                Debug.LogError("Animator is NULL.");
            }
        }

        void FixedUpdate()
        {
            MoveEnemy();
        }

        private void ShootLaser()
        {
            GameObject firedLaser = Instantiate(_enemyLaserPrefab, transform.position, Quaternion.identity);
            Destroy(firedLaser, 6);
        }

        void MoveEnemy()
        {
            transform.Translate(Vector3.down * Time.deltaTime * _enemySpeed);
        }


        private void OnTriggerEnter2D(Collider2D trigger)
        {
            if (trigger.gameObject.CompareTag("Laser_Projectile"))
            {
                GameManager.Instance.UpdateScore(_enemyScoreValue);
                _destroyEnemyAnimator.SetTrigger("IsEnemyDestroyed");

                _explosionSound.Play();
                Destroy(GetComponent<Collider2D>());
                CancelInvoke();
                _enemySpeed = 0.5f;
                Destroy(this.gameObject, 2.8f);
                Destroy(trigger.gameObject);
            }

            if (trigger.gameObject.CompareTag("Player"))
            {
                _enemySpeed = 0.5f;
                _explosionSound.Play();
                _destroyEnemyAnimator.SetTrigger("IsEnemyDestroyed");
                Destroy(GetComponent<Collider2D>());
                CancelInvoke();
                Destroy(this.gameObject, 2.8f);


                if (_playerController != null)
                {
                    _playerController.DestroyLive();
                }
            }
        }
    }
}
