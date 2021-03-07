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
        public int HighScore { get  { return _highScore;  } }

        [SerializeField] public AudioSource _musicLevel;
        private bool _welcomeMessageIsShowed = true;
        public bool WelcomeMessageIsShowed { get { return _welcomeMessageIsShowed; } set { _welcomeMessageIsShowed = value; } }

        private void Update()
        {
            CheckForRestart();
        }
        private void Start()
        {
            InitGameStats();
            InitializeGUI();
            _highScore = 0;
        }
        public void ResetHighScore()
        {
            _highScore = 0;
        }
        public void ToggleWelcomeMessage()
        {
            _welcomeMessageIsShowed = true;
            UIManager.Instance.ActivateWelcomeMessage();
        }

        public void PauseGame()
        {
            _isGamePaused = !_isGamePaused;

            if (_isGamePaused)
            {
                Time.timeScale = 0f;
                _musicLevel.Pause();
                UIManager.Instance.PauseGame();
            }
            else
            {
                _musicLevel.Play();
                Time.timeScale = 1f;
                UIManager.Instance.ResumeGame();
            }
        }

        private void InitGameStats()
        {           
            _lives = 3;
            _score = 0;
            _isGameOver = false;
            _isGamePaused = false;
            //UIM Hint Anzeige
        }

        public void ResetLevel()
        {
            InitGameStats();
            _musicLevel.Play();
            SpawnManager.Instance.StopAllCoroutines();
            SpawnManager.Instance.SetAllListsInactive();
            InitializeGUI();
        }

        private void CheckForRestart()
        {
            if (_isGameOver && Input.GetKeyDown(KeyCode.R))
            {
                InitGameStats();
                InitializeGUI();
                _musicLevel.Play();
                SceneManager.LoadSceneAsync(1);        
            }
        }

        private void InitializeGUI()
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
