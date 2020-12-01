using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 4);
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
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Laser_Projectile"))
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            PlayerController _player = collision.transform.GetComponent<PlayerController>();

            if (_player != null)
            {
                _player.DestroyLive();
            }
        }
    }
}
