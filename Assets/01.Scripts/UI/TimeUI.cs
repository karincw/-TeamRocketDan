using System;
using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace JSY
{
    public class TimeUI : MonoBehaviour
    {
        [SerializeField] private Button skipButton;

        private TextMeshProUGUI timeText;
        private Coroutine waveRoutine;
        private RectTransform rectTrm => transform as RectTransform;

        private void Awake()
        {
            timeText = transform.Find("TimeTxt").GetComponent<TextMeshProUGUI>();
            WaveManager.Instance.OnChangeTurnEvent += WaveDelay;
            WaveManager.Instance.OnStartBossTurnEvent += BossTimeLimit;
            skipButton.onClick.AddListener(SkipWaveCoolTime);
        }

        private void WaveDelay()
        {
            skipButton.gameObject.SetActive(true);
            if (waveRoutine != null) 
                StopCoroutine(waveRoutine);
            Sequence seq = DOTween.Sequence();
            seq.AppendCallback(() =>
            {
                waveRoutine = StartCoroutine(SetTimePanel(WaveManager.Instance.GetWave().waveDelay));
            });
            seq.AppendInterval(WaveManager.Instance.GetWave().waveDelay);
            seq.AppendCallback(() =>
            {
                WaveManager.Instance.InvokeStartTurn();
                skipButton.gameObject.SetActive(false);
            });
        }

        private void BossTimeLimit()
        {
            if (waveRoutine != null)
                StopCoroutine(waveRoutine);
            Sequence seq = DOTween.Sequence();
            seq.AppendCallback(() =>
            {
                waveRoutine = StartCoroutine(SetTimePanel(WaveManager.Instance.GetWave().bossTimeLimit));
            });
            seq.AppendInterval(WaveManager.Instance.GetWave().bossTimeLimit);
            seq.AppendCallback(() =>
            {
                if (EnemyCountUI.Instance.isAllDead())
                    WaveManager.Instance.TurnEnd();
                else
                    EnemyCountUI.Instance.GameOver();
            });
        }

        private IEnumerator SetTimePanel(int time)
        {
            rectTrm.DOAnchorPosY(-100, 0.5f);
            var waitTime = new WaitForSeconds(1f);
            for (int i = 0; i <= time; i++)
            {
                timeText.text = time - i + "ÃÊ";
                yield return waitTime;
            }
            rectTrm.DOAnchorPosY(0, 0.5f);
        }

        public void SkipWaveCoolTime()
        {
            StopCoroutine(waveRoutine);
            rectTrm.DOAnchorPosY(0, 0.5f);
            WaveManager.Instance.InvokeStartTurn();
            skipButton.gameObject.SetActive(false);
        }
    }
}
