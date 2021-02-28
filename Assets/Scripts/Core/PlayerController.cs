using StarShip01.Manager;
using System.Collections;
using UnityEngine;

namespace StarShip01.Core
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D _playerRb;
        [SerializeField] private float _playerVerticalSpeed;
        [SerializeField] private float _playerHorizontalSpeed;
        [SerializeField] private float _speedBoostMultiplier;
        [SerializeField] private GameObject _laserPrefab;
        [SerializeField] private GameObject _tripleLaserPrefab;
        [SerializeField] private GameObject _laserContainer;
        [SerializeField] private GameObject _playerShield;
        [SerializeField] private GameObject _fireOnLeftWing, _fireOnRightWing;
        [SerializeField] private float _fireRate = 0.25f;
        private float _nextFire = 0.0f;
        private float _xMaxPosition = 9.5f;
        private float _yMinPosition = -4.0f;
        private float _yMaxPosition = -1.0f;
        private float _horizontalInput;
        private float _verticalInput;
        private bool _isTripleShotActive = false;
        private bool _isSpeedBoostActive = false;
        private bool _isShieldActive = false;
        private AudioSource _laserShootAudioClip;
        private Animator _playerAnimator;

        void Start()
        {
            _playerRb = GetComponent<Rigidbody2D>();
            _laserShootAudioClip = GetComponent<AudioSource>();
            _playerAnimator = GetComponent<Animator>();

            if (_laserShootAudioClip == null)
            {
                Debug.LogError("The AudioSource on the Player is null.");
            }
        }

        void Update()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
            MovePlayer();
            ShootLaser();
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
                    GameObject _tripleLaser = Instantiate(_tripleLaserPrefab, transform.position + new Vector3(-0.1f, 0.0f, 0), Quaternion.identity);
                    _tripleLaser.transform.parent = _laserContainer.transform;
                }
                else
                {
                    GameObject _laser = Instantiate(_laserPrefab, transform.position + new Vector3(0.0f, 0.0f, 0), Quaternion.identity);
                    _laser.transform.parent = _laserContainer.transform;
                }
                _nextFire = Time.time + _fireRate;
                _laserShootAudioClip.Play();
            }
        }
        public void DestroyLive()
        {
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
                    Destroy(this.gameObject);
                    break;
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy_Laser_Projectile"))
            {
                DestroyLive();
                Destroy(collision.gameObject);
            }
        }

        #region PowerUps - activation + timer
        public void SetTripleShotActive()
        {
            _isTripleShotActive = true;
            StartCoroutine(SetPowerUpActiveTime());
        }

        IEnumerator SetPowerUpActiveTime()
        {
            yield return new WaitForSeconds(5.0f);
            _isTripleShotActive = false;
        }

        public void SetSpeedBoostActive()
        {
            _isSpeedBoostActive = true;
            _playerHorizontalSpeed *= _speedBoostMultiplier;
            _speedBoostMultiplier *= _speedBoostMultiplier;
            StartCoroutine(SetSpeedBoostActiveTime());
        }

        IEnumerator SetSpeedBoostActiveTime()
        {
            yield return new WaitForSeconds(5.0f);
            _isSpeedBoostActive = false;
            _playerHorizontalSpeed /= _speedBoostMultiplier;
            _speedBoostMultiplier /= _speedBoostMultiplier;
        }

        public void SetShieldActive()
        {
            _isShieldActive = true;
            _playerShield.SetActive(true);
        }
        #endregion
    }
}
