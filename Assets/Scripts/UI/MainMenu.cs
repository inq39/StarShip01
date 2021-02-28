using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StarShip01.UI
{
    public class MainMenu : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
