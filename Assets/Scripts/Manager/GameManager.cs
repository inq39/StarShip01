using UnityEngine;
using UnityEngine.SceneManagement;

namespace StarShip01.Manager
{
    public class GameManager : MonoSingleton<GameManager>
    {      
        private bool _isGameOver;
        public bool IsGameOver { get { return _isGameOver; } }
        private int _score = 0;
        public int Score { get { return _score; } }
        //private float _playTime;
        //private float _bestTime;
        [SerializeField] private int _lives;
        public int Lives { get { return _lives; } }

        private void Update()
        {
            CheckForRestart();
        }

        private void CheckForRestart()
        {
            if (_isGameOver && Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadSceneAsync(1);
                ResetLevel();
            }
        }

        private void ResetLevel()
        {
            _lives = 3;
            _score = 0;
            _isGameOver = false;
            UIManager.Instance.StartNewLevel();
            UIManager.Instance.UpdateScoreText();
            UIManager.Instance.UpdateLivesStatus();
        }

        public void GameOver()
        {
            _isGameOver = true;
            SpawnManager.Instance.StopAllCoroutines();
            UIManager.Instance.SetGameOverText();
        }

        public void UpdateScore(int newScore)
        {
            _score += newScore;

            UIManager.Instance.UpdateScoreText();
        }

        public void UpdateLives()
        {
            _lives--;
            UIManager.Instance.UpdateLivesStatus();
            
            if (_lives <= 0)
            {
                GameOver();
            }
        }
    }
}
