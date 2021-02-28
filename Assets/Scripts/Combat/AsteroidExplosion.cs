using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarShip01.Combat
{
    public class AsteroidExplosion : MonoBehaviour
    {
        void Start()
        {
            Destroy(this.gameObject, 3f);
        }
    }
}
