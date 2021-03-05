using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StarShip01.Manager;

namespace StarShip01.UI
{
    public class MainMenu : MonoBehaviour
    {
        public void StartGame()
        {
            InitLevel();
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void ResumeGame()
        {
            GameManager.Instance.PauseGame();
        }

        public void RestartLevel()
        {
            InitLevel();
            GameManager.Instance.PauseGame();

        }

        private void InitLevel()
        {
            GameManager.Instance.InitializeGUI();
            SpawnManager.Instance.StopAllCoroutines();
            SpawnManager.Instance.SetAllListsInactive();
            SceneManager.LoadSceneAsync(1);

        }

        public void ReturnToMainMenu()
        {
            SceneManager.LoadSceneAsync(0);
            GameManager.Instance.ReturnToMainMenu();
        }

        public void StartGameFromMainMenu()
        {
            SceneManager.LoadSceneAsync(1);
        }

    }
}
