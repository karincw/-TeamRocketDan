using Leo.Entity.SO;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JSY
{
    public class WaveManager : MonoSingleton<WaveManager>
    {
        public event Action OnChangeTurnEvent;
        public event Action OnStartTurnEvent;
        public event Action OnStartBossTurnEvent;

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
            if (waveRepeatCount > waves.Count - 1)
                waveRepeatCount = 0;
            OnChangeTurnEvent?.Invoke();
        }

        public void InvokeStartTurn()
        {
            waveCount++;
            if (waves[waveRepeatCount].isBoss)
            {
                OnStartBossTurnEvent?.Invoke();
                NoticeUI.Instance.Notice("보스가 출몰합니다!");
            }
            else
                NoticeUI.Instance.Notice("적이 출몰합니다!");

            OnStartTurnEvent?.Invoke();
        }
    }
}