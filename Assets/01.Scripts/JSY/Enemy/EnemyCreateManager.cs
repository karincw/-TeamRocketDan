using Karin.PoolingSystem;
using Leo.Core;
using Leo.Entity.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

namespace JSY
{
    public class EnemyCreateManager : MonoSingleton<EnemyCreateManager>
    {
        [SerializeField] public Transform enemyParent;
        [SerializeField] private Transform startTrm;
        [SerializeField] private List<MovePoint> movePoints = new List<MovePoint>();

        protected override void Awake()
        {
            WaveManager.Instance.OnStartTurnEvent += HandleStartTurnEvent;
        }


        private void OnDestroy()
        {
            WaveManager.Instance.OnStartTurnEvent -= HandleStartTurnEvent;
        }

        private void HandleStartTurnEvent()
        {
            StartCoroutine(CreateEnemy());
        }

        private IEnumerator CreateEnemy()
        {
            WaveSO wave = WaveManager.Instance.GetWave();
            var coolTime = new WaitForSeconds(wave.spawnDelay);
            foreach (var enemy in wave.enemies)
            {
                
                Enemy obj = PoolManager.Instance.Pop(enemy) as Enemy;
                if (obj is not MiniBoss)
                {
                    UIManager.Instance.EnemyCountUI.UpdateCount(1);
                }
                if (obj is Boss.Boss)
                {
                    StartCoroutine(BossWarning());
                }
                obj.transform.position = startTrm.position;
                obj.transform.parent = enemyParent;
                obj.SetMovePoints(movePoints);
                yield return coolTime;
            }
            if (!wave.isBoss)
                WaveManager.Instance.TurnEnd();
        }

        private IEnumerator BossWarning()
        {
            for (int i = 0; i < 3; i++)
            {
                VolumeManager.Instance.GetComponent<Vignette>().color.DOColor(Color.red, 0.5f,
                    () => VolumeManager.Instance.GetComponent<Vignette>().color.DOColor(Color.black, 0.5f));
                yield return new WaitForSeconds(1f);
            }
        }

        public void UpdatePoints(Transform startTrm, List<MovePoint> movePoints)
        {
            this.movePoints.Clear();
            this.startTrm = startTrm;
            this.movePoints = movePoints.ToList();
        }

        public void DeadEnemy()
        {
            gameObject.SetActive(false);
        }
    }
}
