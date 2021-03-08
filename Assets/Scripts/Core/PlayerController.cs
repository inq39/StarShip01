using StarShip01.Manager;
using System;
using System.Collections;
using UnityEngine;

namespace StarShip01.Core
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Values")]     
        [SerializeField] private float _playerVerticalSpeed;
        [SerializeField] private float _playerHorizontalSpeed;
        [SerializeField] private float _speedBoostMultiplier;
        [SerializeField] private float _speedBoostDuration;
        [SerializeField] private float _tripleShotDuration;
        [SerializeField] private float _fireRate; // 0.25f
        [SerializeField] private float _xMaxPosition = 9.5f; // 9.5f
        [SerializeField] private float _yMinPosition = -4.0f; // -4.0f
        [SerializeField] private float _yMaxPosition = -1.0f; // -1.0f

        [Header("Prefabs and Container")]
        [SerializeField] private GameObject _laserPrefab;
        [SerializeField] private GameObject _tripleLaserPrefab;
        [SerializeField] private GameObject _playerShield;
        [SerializeField] private GameObject _fireOnLeftWing, _fireOnRightWing;
        [SerializeField] private GameObject _playerExplosionAnimation;
        [SerializeField] private Animator _cameraShake;

        private float _nextFire = 0.0f;
        private float _horizontalInput;
        private float _verticalInput;
        private bool _isTripleShotActive = false;
        private bool _isSpeedBoostActive = false;
        private bool _isShieldActive = false;

        private Rigidbody2D _playerRb;
        private AudioSource _laserShootAudioClip;
        private Animator _playerAnimator;

        void Start()
        {
            _playerRb = GetComponent<Rigidbody2D>();
            if (_playerRb == null)          
                Debug.LogError("The Player_RB on the Player is null.");           

            _playerAnimator = GetComponent<Animator>();
            if (_playerAnimator == null)
                Debug.LogError("The Player Animator on the Player is null.");          

            _laserShootAudioClip = GetComponent<AudioSource>();
            if (_laserShootAudioClip == null)
                Debug.LogError("The AudioSource on the Player is null.");

        }

        void Update()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");

            if (!GameManager.Instance.WelcomeMessageIsShowed)
            {
                MovePlayer();
                ShootLaser();
                PauseGame();
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    UIManager.Instance.DeactivateWelcomeMessage();
                    GameManager.Instance.WelcomeMessageIsShowed = false;
                }
            }
        }

        private void PauseGame()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.PauseGame();
            }
        }

        void MovePlayer()
        {
            _playerRb.AddForce(Vector3.up * _verticalInput * Time.deltaTime * _playerVerticalSpeed);
            _playerRb.AddForce(Vector3.right * _horizontalInput * Time.deltaTime * _playerHorizontalSpeed);

            PlayerAnimation();

            if (transform.position.x > _xMaxPosition)
            {
                transform.position = new Vector3(-_xMaxPosition, transform.position.y, 0);
            }
            else if (transform.position.x < -_xMaxPosition)
            {
                transform.position = new Vector3(_xMaxPosition, transform.position.y, 0);
            }

            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _yMinPosition, _yMaxPosition), 0);
        }

        private void PlayerAnimation()
        {
            if (_horizontalInput < 0)
            {
                _playerAnimator.ResetTrigger("RollMiddle");
                _playerAnimator.ResetTrigger("RollRight");
                _playerAnimator.SetTrigger("RollLeft");
            }
            else if (_horizontalInput > 0)
            {
                _playerAnimator.ResetTrigger("RollLeft");
                _playerAnimator.ResetTrigger("RollMiddle");
                _playerAnimator.SetTrigger("RollRight");
            }
            else
            {
                _playerAnimator.ResetTrigger("RollLeft");
                _playerAnimator.ResetTrigger("RollRight");
                _playerAnimator.SetTrigger("RollMiddle");
            }
        }

        void ShootLaser()
        {
            if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
            {
                if (_isTripleShotActive)
                {
                    GameObject tripleLaser = SpawnManager.Instance.RequestTripleLaser();
                    tripleLaser.transform.position = transform.position + new Vector3(-0.1f, 0.0f, 0);
                    tripleLaser.transform.rotation = Quaternion.identity;
                }
                else
                {
                    GameObject laser = SpawnManager.Instance.RequestPlayerLaser();
                    laser.transform.position = transform.position + new Vector3(0.0f, 0.0f, 0);
                    laser.transform.rotation = Quaternion.identity;
                }

                _nextFire = Time.time + _fireRate;
                _laserShootAudioClip.Play();
            }
        }
        public void DestroyLive()
        {
            _cameraShake.SetTrigger("Camera_Shake");

            if (_isShieldActive)
            {
                _isShieldActive = false;
                _playerShield.SetActive(false);
                return;
            }

            GameManager.Instance.UpdateLives();

            switch (GameManager.Instance.Lives)
            {
                case 3:
                    break;
                case 2:
                    _fireOnRightWing.SetActive(true);
                    break;
                case 1:
                    _fireOnLeftWing.SetActive(true);
                    break;
                case 0:
                    GetComponent<Collider2D>().enabled = false;
                    Instantiate(_playerExplosionAnimation, transform.position, Quaternion.identity);
                    Destroy(this.gameObject, 0.15f);
                    break;
            }
        }
        private void OnTriggerEnter2D(Collider2D trigger)
        {
            if (trigger.gameObject.CompareTag("Enemy_Laser_Projectile"))
            {
                DestroyLive();
                trigger.gameObject.SetActive(false);
            }
            else if (trigger.gameObject.CompareTag("Enemy"))
            {
                DestroyLive();
            }
            else if (trigger.gameObject.CompareTag("PowerUp"))
            {
                int ID = trigger.GetComponent<PowerUp>().PowerUpID;
                switch (ID)
                {
                    case 0:
                        SetShieldActive();
                        break;
                    case 1:
                        SetSpeedBoostActive();
                        break;
                    case 2:
                        SetTripleShotActive();
                        break;
                    default:
                        Debug.Log("default");
                        break;
                }
            }
        }

        #region PowerUps - activation + timer
        private void SetTripleShotActive()
        {
            _isTripleShotActive = true;
            StartCoroutine(SetTripleShotActiveTime());
        }

        IEnumerator SetTripleShotActiveTime()
        {    
            yield return new WaitForSeconds(_tripleShotDuration);
            _isTripleShotActive = false;
        }

        private void SetSpeedBoostActive()
        {
            _isSpeedBoostActive = true;
            _playerHorizontalSpeed *= _speedBoostMultiplier;
            _playerVerticalSpeed *= _speedBoostMultiplier;
            StartCoroutine(SetSpeedBoostActiveTime());
        }

        IEnumerator SetSpeedBoostActiveTime()
        {
            yield return new WaitForSeconds(_speedBoostDuration);
            _isSpeedBoostActive = false;
            _playerHorizontalSpeed /= _speedBoostMultiplier;
            _playerVerticalSpeed /= _speedBoostMultiplier;
        }

        private void SetShieldActive()
        {
            _isShieldActive = true;
            _playerShield.SetActive(true);
        }
        #endregion
    }
}
