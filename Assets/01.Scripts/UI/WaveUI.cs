using System;
using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    private TextMeshProUGUI waveText;
    private int waveCount = -1;

    private void Awake()
    {
        waveText = GetComponentInChildren<TextMeshProUGUI>();
        HandleStartTurnEvent();
        WaveManager.Instance.OnStartTurnEvent += HandleStartTurnEvent;
    }

    private void HandleStartTurnEvent()
    {
        waveCount++;
        waveText.text = "���̺� " + waveCount;
    }
}