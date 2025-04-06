using Leo.Entity.SO;
using Leo.Sound;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JSY
{
    public class WaveManager : MonoSingleton<WaveManager>
    {
        [SerializeField] private SoundObject warningSound;
        public event Action OnChangeTurnEvent;
        public event Action OnStartTurnEvent;
        public event Action OnStartBossTurnEvent;
        public event Action OnStoryEndEvent;

        [SerializeField] private List<WaveSO> waves = new List<WaveSO>();
        [SerializeField] private bool isRepeatMode = true;
        private int waveRepeatCount = 0;
        private int waveCount = 0;
        private int repeatCount = 0;
        private int add = 40;

        public int PoweredHp(int hp) => repeatCount != 0 ? hp * (repeatCount + 1) + 40 * repeatCount : hp;
        public int PoweredReward(int r) => repeatCount != 0 ? r + 8 * repeatCount : r;
        protected override void Awake()
        {
        }

        private void Start()
        {
            OnChangeTurnEvent?.Invoke();
        }

        public WaveSO GetWave() => waves[waveRepeatCount];
        public int GetWaveCount() => waveCount;
        public int GetRepeatCount() => repeatCount;

        public void TurnEnd()
        {
            waveRepeatCount++;
            if (waveRepeatCount > waves.Count - 1)
            {
                if(!isRepeatMode)
                {
                    OnStoryEndEvent?.Invoke();
                    return;
                }
                repeatCount++;
                waveRepeatCount = 0;
            }

            OnChangeTurnEvent?.Invoke();
        }

        public void InvokeStartTurn()
        {
            waveCount++;
            if (waves[waveRepeatCount].isBoss)
            {
                OnStartBossTurnEvent?.Invoke();
                UIManager.Instance.NoticeUI.Notice("보스가 출몰합니다!");
                warningSound.Play();
            }
            else
                UIManager.Instance.NoticeUI.Notice("적이 출몰합니다!");

            OnStartTurnEvent?.Invoke();
        }
    }
}