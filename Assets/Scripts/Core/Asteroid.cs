using UnityEngine;
using StarShip01.Manager;

namespace StarShip01.Core
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private float _rotateSpeed = 6.0f;
        [SerializeField] private float _speedFactorCollision = 0.2f;
        [SerializeField] private GameObject _asteroidExplosionAnimation;

        void Update()
        {
            transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Laser_Projectile")
            {
                _rotateSpeed *= _speedFactorCollision;
                Instantiate(_asteroidExplosionAnimation, transform.position, Quaternion.identity);
                GameManager.Instance._musicLevel.Play();
                SpawnManager.Instance.StartSpawning();
                other.gameObject.SetActive(false);
                Destroy(this.gameObject, 0.15f);
            }
        }
    }
}
