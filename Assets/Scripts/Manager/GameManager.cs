using UnityEngine;
using UnityEngine.SceneManagement;

namespace StarShip01.Manager
{
    public class GameManager : MonoSingleton<GameManager>
    {      
        private bool _isGameOver;
        public bool IsGameOver { get { return _isGameOver; } }
        private int _score;
        public int Score { get { return _score; } }

        private int _lives;
        public int Lives { get { return _lives; } }
        private int _highScore;
        public int HighScore { get { return _highScore; } }


        private void Update()
        {
            CheckForRestart();
        }
        private void Start()
        {
            InitializeLevel();
        }

        private void CheckForRestart()
        {
            if (_isGameOver && Input.GetKeyDown(KeyCode.R))
            {
                InitializeLevel();
                SceneManager.LoadSceneAsync(1);        
            }
        }

        private void InitializeLevel()
        {
            _lives = 3;
            _score = 0;
            _isGameOver = false;
            UIManager.Instance.StartNewLevel();
            UIManager.Instance.UpdateScoreText();
            UIManager.Instance.UpdateLivesStatus();
            UIManager.Instance.UpdateHighScoreText();
        }

        public void GameOver()
        {
            _isGameOver = true;
            SpawnManager.Instance.StopAllCoroutines();
            SpawnManager.Instance.SetAllListsInactive();
            UIManager.Instance.SetGameOverText();
        }

        public void UpdateScore(int newScore)
        {
            _score += newScore;

            UIManager.Instance.UpdateScoreText();

            if (_score > _highScore)
            {
                _highScore = _score;
                UIManager.Instance.UpdateHighScoreText();
            }
        }

        public void UpdateLives()
        {
            _lives--;

            if (_lives < 0)
            {
                _lives = 0;
            }

            UIManager.Instance.UpdateLivesStatus();
            
            if (_lives == 0)
            {
                GameOver();
            }
        }
    }
}
