using Karin.PoolingSystem;
using Leo.Entity.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JSY
{
    public class EnemyCreateManager : MonoSingleton<EnemyCreateManager>
    {
        [SerializeField] private Transform enemyParent;
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
                EnemyCountUI.Instance.UpdateCount(1);
                Enemy obj = PoolManager.Instance.Pop(enemy) as Enemy;
                obj.transform.position = startTrm.position;
                obj.transform.parent = enemyParent;
                obj.SetMovePoints(movePoints);
                yield return coolTime;
            }
            if (!wave.isBoss)
                WaveManager.Instance.TurnEnd();
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
