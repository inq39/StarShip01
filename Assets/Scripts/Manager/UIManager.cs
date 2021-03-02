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
        [SerializeField] private Image _playerStatus;
        [SerializeField] private Sprite[] _playerLiveSprites;
        [SerializeField] private TextMeshProUGUI _gameOverText;
        [SerializeField] private TextMeshProUGUI _restartLevelText;

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

        public void UpdateScoreText()
        {
            _scoreText.SetText("Score: " + GameManager.Instance.Score.ToString());
        }

        public void UpdateLivesStatus()
        {
            _playerStatus.sprite = _playerLiveSprites[GameManager.Instance.Lives];
        }

        public void SetGameOverText()
        {
            _gameOverText.gameObject.SetActive(true);
            _restartLevelText.gameObject.SetActive(true);
        }
    }
}