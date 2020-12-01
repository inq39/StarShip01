using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRb;
    [SerializeField]
    private int _lives = 3;
    Vector3 _offset;
    [SerializeField] 
    private float _playerVerticalSpeed = 50.0f;
    [SerializeField] 
    private float _playerHorizontalSpeed = 100.0f;
    [SerializeField] 
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _nextFire = 0.0f;
    private float _xMinPosition = -11.5f;
    private float _xMaxPosition = 11.5f;
    private float _yMinPosition = -4.0f;
    private float _yMaxPosition = -1.0f;
    private float _horizontalInput;
    private float _verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        MovePlayer();
        ShootLaser();
    }

    void MovePlayer()
    {
        _playerRb.AddForce(Vector3.up * _verticalInput * Time.deltaTime * _playerVerticalSpeed);
        _playerRb.AddForce(Vector3.right * _horizontalInput * Time.deltaTime * _playerHorizontalSpeed);
        
        if (transform.position.x > _xMaxPosition)
        {
            transform.position = new Vector3(_xMinPosition, transform.position.y, 0);
        }
        else if (transform.position.x < _xMinPosition)
        {
            transform.position = new Vector3(_xMaxPosition, transform.position.y, 0);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _yMinPosition, _yMaxPosition), 0);
    }

    void ShootLaser()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
        {
            _offset = transform.position + new Vector3(0, 0.75f, 0);
            Instantiate(_laserPrefab, _offset, Quaternion.identity);
            _nextFire = Time.time + _fireRate;
        }

    }
    public void DestroyLive()
    {
        _lives--;

        if (_lives <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
