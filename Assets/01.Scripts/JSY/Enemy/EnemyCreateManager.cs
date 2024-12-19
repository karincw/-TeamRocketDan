using Leo.Entity.SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JSY
{
    public class EnemyCreateManager : MonoBehaviour
    {
        [SerializeField] private Transform enemyParent;
        [SerializeField] private Transform startTrm;
        [SerializeField] private List<MovePoint> movePoints = new List<MovePoint>();

        private void Awake()
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
            if(!wave.isBoss)
                WaveManager.Instance.TurnEnd();
        }
    }
}
