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
            var coolTime = new WaitForSeconds(WaveManager.Instance.GetWave().spawnDelay);
            foreach (Enemy enemy in WaveManager.Instance.GetWave().enemies)
            {
                EnemyCountUI.Instance.UpdateCount(1);
                Enemy obj = Instantiate(enemy, startTrm.position, Quaternion.identity, enemyParent);
                obj.SetMovePoints(movePoints);
                yield return coolTime;
            }
            WaveManager.Instance.TurnEnd();
        }
    }
}
