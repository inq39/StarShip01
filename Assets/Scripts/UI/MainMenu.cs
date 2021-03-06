using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StarShip01.Manager;

namespace StarShip01.UI
{
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadSceneAsync(1);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void ResumeLevel()
        {
            GameManager.Instance.PauseGame();
        }

        public void RestartLevel()
        {
            GameManager.Instance.PauseGame();
            GameManager.Instance.ResetLevel();
            SceneManager.LoadSceneAsync(1);
        }

        public void ReturnToMainMenu()
        {
            GameManager.Instance.ResetHighScore();
            GameManager.Instance.PauseGame();           
            GameManager.Instance.ResetLevel();
            SceneManager.LoadSceneAsync(0);
        }
    }
}
