using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JSY
{
    public class EnemyCreateManager : MonoBehaviour
    {
        [SerializeField] private Transform enemyParent;
        [SerializeField] private Enemy enemyPrefab;
        [SerializeField] private List<MovePoint> movePoints = new List<MovePoint>();

        private void Update()
        {
            if(Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                CreateEnemy();
            }
        }

        private void CreateEnemy()
        {
            Enemy obj = Instantiate(enemyPrefab, movePoints[0].transform.position, Quaternion.identity, enemyParent);
            obj.SetMovePoints(movePoints);
        }
    }
}
