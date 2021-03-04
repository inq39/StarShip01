using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarShip01.Manager
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        [SerializeField] private AudioSource _gameMusic;
        [SerializeField] private AudioSource _mainMenuMusic;
    }
}