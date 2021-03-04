using UnityEngine;

namespace StarShip01.Core
{
    public class PowerUp : MonoBehaviour
    {
        [SerializeField] private float _powerUpSpeed = 3.0f;
        [SerializeField] private int _powerupID;
        public int PowerUpID { get { return _powerupID; } }
        private PlayerController _playerController;
        [SerializeField] private AudioClip _powerUpAudioClip;
        [SerializeField] private float _returnToPoolTime;

        private void Start()
        {
            _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            if (_playerController == null)
                Debug.LogError("PlayerController is NULL.");
        }

        private void OnEnable()
        {
            Invoke("SetPowerUpInactive", _returnToPoolTime);
        }

        private void SetPowerUpInactive()
        {
            this.gameObject.SetActive(false);
        }

        void Update()
        {
            transform.Translate(Vector3.down * Time.deltaTime * _powerUpSpeed);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                AudioSource.PlayClipAtPoint(_powerUpAudioClip, transform.position);
                this.gameObject.SetActive(false);             
            }
        }
    }
}
