using UnityEngine;

namespace StarShip01.Core
{
    public class PowerUp : MonoBehaviour
    {
        [SerializeField] private float _powerUpSpeed = 3.0f;
        private float _positionMinY = -5.0f;
        [SerializeField] private int _powerupID;
        private PlayerController _playerController;
        [SerializeField] private AudioClip _powerUpAudioClip;

        private void Start()
        {
            _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }

        void Update()
        {
            transform.Translate(Vector3.down * Time.deltaTime * _powerUpSpeed);

            if (transform.position.y < _positionMinY)
            {
                this.gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                AudioSource.PlayClipAtPoint(_powerUpAudioClip, transform.position);
                this.gameObject.SetActive(false);

                if (_playerController != null)
                {
                    switch (_powerupID)
                    {
                        case 0:
                            _playerController.SetShieldActive();
                            break;
                        case 1:
                            _playerController.SetSpeedBoostActive();
                            break;
                        case 2:
                            _playerController.SetTripleShotActive();
                            break;
                        default:
                            Debug.Log("default");
                            break;
                    }
                }
            }
        }
    }
}
