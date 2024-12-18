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
    private int waveRepeatCount = 0;
    private int waveCount = 0;
    protected override void Awake()
    {
    }

    private void Start()
    {
        OnChangeTurnEvent?.Invoke();
    }

    public WaveSO GetWave() => waves[waveRepeatCount];
    public int GetWaveCount() => waveCount;

    public void TurnEnd()
    {
        waveRepeatCount++;
        if (waveRepeatCount > waves.Count-1)
            waveRepeatCount = 0;
        OnChangeTurnEvent?.Invoke();
    }

    public void InvokeStartTurn()
    {
        waveCount++;
        OnStartTurnEvent?.Invoke();
    }
}
