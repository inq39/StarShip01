using UnityEngine;
using UnityEngine.SceneManagement;

namespace StarShip01.Manager
{
    public class GameManager : MonoSingleton<GameManager>
    {
        private bool _isGamePaused;
        public bool IsGamePaused { get { return _isGamePaused; } }
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
            InitGameStats();
            InitializeGUI();
        }

        public void PauseGame()
        {
            _isGamePaused = !_isGamePaused;

            if (_isGamePaused)
            {
                Time.timeScale = 0f;
                AudioListener.pause = true;
                UIManager.Instance.PauseGame();
            }
            else
            {
                AudioListener.pause = false;
                Time.timeScale = 1f;
                UIManager.Instance.ResumeGame();
            }
        }

        public void InitGameStats()
        {           
            _highScore = 0;
            _lives = 3;
            _score = 0;
            _isGameOver = false;
            _isGamePaused = false;
            //UIM Hint Anzeige
        }


        public void ReturnToMainMenu()
        {
            InitGameStats();
            AudioListener.pause = false;
            SpawnManager.Instance.StopAllCoroutines();
            SpawnManager.Instance.SetAllListsInactive();
        }

        private void CheckForRestart()
        {
            if (_isGameOver && Input.GetKeyDown(KeyCode.R))
            {
                InitializeGUI();
                SceneManager.LoadSceneAsync(1);        
            }
        }

        public void InitializeGUI()
        {
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
