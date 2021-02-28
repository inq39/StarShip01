using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarShip01.Combat
{
    public class Enemy_Laser : Base_Laser
    {
        protected override void MoveLaserBeam()
        {
            transform.Translate(Vector3.down * Time.deltaTime * _laserSpeed);
        }
    }
}
