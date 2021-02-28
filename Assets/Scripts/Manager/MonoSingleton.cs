using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarShip01.Manager
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T _instance;
        public static T Instance 
        { 
            get 
            {   if (_instance == null)
                {
                    Debug.LogError(typeof(T).ToString() + " is NULL.");
                }
                return _instance; 
            } 
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }   
        }
    }
 
}
