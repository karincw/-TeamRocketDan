using Leo.Entity.SO;
using System.Collections;
using System.Collections.Generic;
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

        private void HandleStartTurnEvent()
        {
            StartCoroutine(CreateEnemy());
        }

        private IEnumerator CreateEnemy()
        {
            WaveSO wave = WaveManager.Instance.GetWave();
            var coolTime = new WaitForSeconds(wave.spawnDelay);
            foreach (Enemy enemy in wave.enemies)
            {
                EnemyCountUI.Instance.UpdateCount(1);
                Enemy obj = Instantiate(enemy, startTrm.position, Quaternion.identity, enemyParent);
                obj.SetMovePoints(movePoints);
                yield return coolTime;
            }
            if (!wave.isBoss)
                WaveManager.Instance.TurnEnd();
        }

        public void UpdatePoints(Transform startTrm,  List<MovePoint> movePoints)
        {
            this.movePoints.Clear();
            this.startTrm = startTrm;
            this.movePoints = movePoints;
        }
    }
}
