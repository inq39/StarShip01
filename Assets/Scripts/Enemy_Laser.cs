using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Laser : MonoBehaviour
{
    [SerializeField] float _laserSpeed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        MoveLaserBeam();
    }

    

    void MoveLaserBeam()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _laserSpeed);
    }
}
