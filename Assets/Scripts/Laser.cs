using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float _laserSpeed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        MoveLaserBeam();
    }

    

    void MoveLaserBeam()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _laserSpeed);
    }
}
