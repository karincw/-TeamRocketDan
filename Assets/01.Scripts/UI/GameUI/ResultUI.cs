using Leo.Sound;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace JSY
{
    public class ResultUI : PopupUI
    {
        [SerializeField] private SoundObject _gameOverSound;
        private TextMeshProUGUI waveText, playTimeText, enemyText;
        private Button titleButton;

        private DateTime startTime, endTime;
        private int deadEnemyCount = 0;
        private bool isEnd = false;

        private void Awake()
        {
            startTime = DateTime.Now;
            Transform resultValueGroup = PanelTrm.Find("PopupUI").Find("ResultValueGroup");
            waveText = resultValueGroup.Find("WaveTxt").GetComponent<TextMeshProUGUI>();
            playTimeText = resultValueGroup.Find("PlayTimeTxt").GetComponent<TextMeshProUGUI>();
            enemyText = resultValueGroup.Find("EnemyTxt").GetComponent<TextMeshProUGUI>();

            titleButton = PanelTrm.Find("PopupUI").Find("ExitBtn").GetComponent<Button>();
            titleButton.onClick.AddListener(HandleExitButton);
        }

        private void HandleExitButton()
        {
            SceneManager.LoadScene("TitleScene");
        }

        public void AddDeadEnemy() => deadEnemyCount++;

        public void GameOver()
        {
            if (isEnd) return;
            isEnd = true;

            EnemyCreateManager.Instance.DeadEnemy();
            _gameOverSound.Play();

            endTime = DateTime.Now;
            TimeSpan playDuration = endTime - startTime;
            string timeStr = playDuration.ToString(@"mm\:ss");

            waveText.text = WaveManager.Instance.GetWaveCount().ToString();
            playTimeText.text = timeStr;
            enemyText.text = deadEnemyCount.ToString();
            OpenPanel();
        }
    }
}
