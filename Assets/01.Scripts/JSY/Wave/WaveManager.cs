using Leo.Entity.SO;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoSingleton<WaveManager>
{
    public event Action OnChangeTurnEvent;
    public event Action OnStartTurnEvent;

    [SerializeField] private List<WaveSO> waves = new List<WaveSO>();
    private int waveCnt = 0;

    private void Awake()
    {
        OnChangeTurnEvent?.Invoke();
    }

    public WaveSO GetWave() => waves[waveCnt];
    public int GetWaveCount() => waveCnt;

    public void TurnEnd()
    {
        waveCnt++;
        if (waveCnt > waves.Count-1)
            waveCnt = 0;
        OnChangeTurnEvent?.Invoke();
    }

    public void InvokeStartTurn()
    {
        OnStartTurnEvent?.Invoke();
        Debug.Log("¤±¤¤¤·¤©");
    }
}
