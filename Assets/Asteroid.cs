using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 6.0f;
    [SerializeField] private GameObject _asteroidExplosionAnimation;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser_Projectile")
        {
            _rotateSpeed = 0;
            Instantiate(_asteroidExplosionAnimation, transform.position, Quaternion.identity);
            
            _gameManager.StartGame();
            Destroy(other.gameObject);
            Destroy(this.gameObject, 0.15f);
        }
    }
}
