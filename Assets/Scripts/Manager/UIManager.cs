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
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Image _playerStatus;
        [SerializeField] private Sprite[] _playerLives;
        [SerializeField] private TextMeshProUGUI _gameOverText;
        [SerializeField] private TextMeshProUGUI _restartLevelText;

        // Update is called once per frame
        void Start()
        {
            _gameOverText.gameObject.SetActive(false);
            _restartLevelText.gameObject.SetActive(false);
        }

        void Update()
        {
            UpdateScoreText();
        }

        void UpdateScoreText()
        {
            int newScore = _playerController.PlayerScore;
            _scoreText.SetText("Score: " + newScore);
        }

        public void UpdateLivesStatus(int currentPlayerLives)
        {
            _playerStatus.sprite = _playerLives[currentPlayerLives];

            if (currentPlayerLives == 0)
            {
                _gameOverText.gameObject.SetActive(true);
                _restartLevelText.gameObject.SetActive(true);
            }
        }
    }
}