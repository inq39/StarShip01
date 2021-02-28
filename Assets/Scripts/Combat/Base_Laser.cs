using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarShip01.Combat
{
    public abstract class Base_Laser : MonoBehaviour
    {
        [SerializeField] protected float _laserSpeed = 20.0f;
        [SerializeField] private float _laserDestroyTime = 5.0f;

        private void Start()
        {
            Destroy(this.gameObject, _laserDestroyTime);
        }

        private void FixedUpdate()
        {
            MoveLaserBeam();
        }

        protected virtual void MoveLaserBeam()
        {
            transform.Translate(Vector3.up * Time.deltaTime * _laserSpeed);
        }
    }
}
