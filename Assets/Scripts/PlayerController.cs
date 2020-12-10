using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _playerRb;
    private GameManager _gameManager;
    [SerializeField] private int _lives = 3;
    [SerializeField] private float _playerVerticalSpeed = 500.0f;
    [SerializeField] private float _playerHorizontalSpeed = 1000.0f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleLaserPrefab;
    [SerializeField] private GameObject _laserContainer;
    [SerializeField] private float _fireRate = 0.25f;
    private float _nextFire = 0.0f;
    private float _xMaxPosition = 9.5f;
    private float _yMinPosition = -4.0f;
    private float _yMaxPosition = -1.0f;
    private float _horizontalInput;
    private float _verticalInput;
    private bool _tripleShotActive = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>(); 
    }

    // Update is called once per frame
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

    void ShootLaser()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
        {       
            if (_tripleShotActive)
            {
                GameObject _tripleLaser = Instantiate(_tripleLaserPrefab, transform.position + new Vector3(-0.1f, 0.0f, 0), Quaternion.identity);
                _tripleLaser.transform.parent = _laserContainer.transform;
            }
            else
            {
                GameObject _laser = Instantiate(_laserPrefab, transform.position + new Vector3(-0.1f, 0.0f, 0), Quaternion.identity);
                _laser.transform.parent = _laserContainer.transform;
            }
            _nextFire = Time.time + _fireRate;

        }

    }
    public void DestroyLive()
    {
        _lives--;

        if (_lives <= 0)
        {
            Destroy(this.gameObject);
            _gameManager.GameOver();
            
        }
    }

    public void SetTripleShotActive()
    {
        _tripleShotActive = true;
        StartCoroutine(SetPowerUpActiveTime());
    }

    IEnumerator SetPowerUpActiveTime()
    {
        yield return new WaitForSeconds(5.0f);
        _tripleShotActive = false;
    }
}
