﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _powerUpSpeed = 3.0f;
    private float _positionMinY = -5.0f;
    [SerializeField] private int _powerupID;
    private PlayerController _playerController;
    // Start is called before the first frame update

    private void Start()
    {
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();  
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _powerUpSpeed);

        if (transform.position.y < _positionMinY)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);

            if (_playerController != null)
            {
                switch (_powerupID)
                {
                    case 0:  
                        _playerController.SetTripleShotActive();
                        break;
                    case 1:
                        _playerController.SetSpeedBoostActive();
                        break;
                    case 2:
                        _playerController.SetShieldActive();
                        break;
                    default:
                        Debug.Log("default");
                        break;
                }
            }
        } 
    }
}