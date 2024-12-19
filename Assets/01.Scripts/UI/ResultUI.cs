using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace JSY
{
    public class ResultUI : MonoSingleton<ResultUI>
    {
        [SerializeField] private Image background;
        [SerializeField] private RectTransform panel;
        [SerializeField] private Transform resultValueGroup;
        private TextMeshProUGUI waveText, playTimeText, enemyText;

        private DateTime startTime, endTime;
        private int deadEnemyCount = 0;
        private float durationTime = 0.5f;
        private bool isEnd = false;

        protected override void Awake()
        {
            startTime = DateTime.Now;
            waveText = resultValueGroup.Find("WaveTxt").GetComponent<TextMeshProUGUI>();
            playTimeText = resultValueGroup.Find("PlayTimeTxt").GetComponent<TextMeshProUGUI>();
            enemyText = resultValueGroup.Find("EnemyTxt").GetComponent<TextMeshProUGUI>();
        }

        public void AddDeadEnemy() => deadEnemyCount++;

        public void GameOver()
        {
            endTime = DateTime.Now;
            TimeSpan playDuration = endTime - startTime;
            string timeStr = playDuration.ToString(@"mm\:ss");

            waveText.text = WaveManager.Instance.GetWaveCount().ToString();
            playTimeText.text = timeStr;
            enemyText.text = deadEnemyCount.ToString();
            OpenPanel();
        }

        private void OpenPanel()
        {
            if (isEnd) return;
            isEnd = true;

            Debug.Log("asdf");
            background.gameObject.SetActive(true);
            panel.gameObject.SetActive(true);

            Sequence seq = DOTween.Sequence();
            seq.Append(background.DOFade(0.6f, durationTime));
            seq.Join(panel.DOScale(1f, durationTime));
        }
    }
}
