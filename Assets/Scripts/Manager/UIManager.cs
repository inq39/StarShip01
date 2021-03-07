using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace StarShip01.Manager
{
    public class UIManager : MonoSingleton<UIManager>
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _highScoreText;
        [SerializeField] private Image _playerStatus;
        [SerializeField] private Sprite[] _playerLiveSprites;
        [SerializeField] private Color[] _playerLiveColor;
        [SerializeField] private GameObject _gameOverText;
        [SerializeField] private TextMeshProUGUI _restartLevelText;
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private GameObject _GUI;
        [SerializeField] private GameObject _introText;

        // Update is called once per frame
        void Start()
        {
            StartNewLevel();
        }

        public void StartNewLevel()
        {
            _gameOverText.gameObject.SetActive(false);
            _restartLevelText.gameObject.SetActive(false);
        }

        public void PauseGame()
        {
            _pauseMenu.SetActive(true);
            _GUI.SetActive(false);
        }

        public void DeactivateWelcomeMessage()
        {
            _introText.SetActive(false);
        }
        public void ActivateWelcomeMessage()
        {
            _introText.SetActive(true);
        }

        public void ResumeGame()
        {
            _pauseMenu.SetActive(false);
            _GUI.SetActive(true);
        }

        public void UpdateScoreText()
        {
            _scoreText.SetText("Score: " + GameManager.Instance.Score.ToString());
        }

        public void UpdateLivesStatus()
        {
            int lives = GameManager.Instance.Lives;
            _playerStatus.sprite = _playerLiveSprites[lives];
            _playerStatus.color = _playerLiveColor[lives];
        }

        public void SetGameOverText()
        {
            _gameOverText.gameObject.SetActive(true);
            _restartLevelText.gameObject.SetActive(true);
        }

        public void UpdateHighScoreText()
        {
            _highScoreText.SetText("HighScore: " + GameManager.Instance.HighScore.ToString());
        }
    }
}