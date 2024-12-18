using System;
using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    [SerializeField] private Button skipButton;

    private TextMeshProUGUI timeText;
    private Coroutine waveRoutine;
    private RectTransform rectTrm => transform as RectTransform;

    private void Awake()
    {
        timeText = transform.Find("TimeTxt").GetComponent<TextMeshProUGUI>();
        WaveManager.Instance.OnChangeTurnEvent += PlayDelay;
        skipButton.onClick.AddListener(SkipWaveCoolTime);
    }

    private void PlayDelay()
    {
        waveRoutine = StartCoroutine(WaveDelay());
    }

    private IEnumerator WaveDelay()
    {
        skipButton.gameObject.SetActive(true);
        var waveCoolTime = WaveManager.Instance.GetWave().waveDelay;
        rectTrm.DOAnchorPosY(-100, 0.5f);
        var waitTime = new WaitForSeconds(1f);
        for(int i = 0; i <  waveCoolTime; i++)
        {
            timeText.text = waveCoolTime - i + "ÃÊ";
            yield return waitTime;
        }
        rectTrm.DOAnchorPosY(0, 0.5f);
        WaveManager.Instance.InvokeStartTurn();
        skipButton.gameObject.SetActive(false);
    }

    public void SkipWaveCoolTime()
    {
        StopCoroutine(waveRoutine);
        rectTrm.DOAnchorPosY(0, 0.5f);
        WaveManager.Instance.InvokeStartTurn();
        skipButton.gameObject.SetActive(false);
    }
}
