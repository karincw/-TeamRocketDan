using System;
using System.Collections.Generic;
using UnityEngine;

namespace JSY
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]private List<MovePoint> movePoints = new List<MovePoint>();
        private int value;

        private bool isTeleport;
        public void SetMovePoints(List<MovePoint> movePoints) => this.movePoints = movePoints;

        protected virtual void Update()
        {
            if(Vector2.Distance(transform.position, movePoints[value].transform.position) <= 0.01f)
            {
                if (movePoints[value].isTeleport)
                    isTeleport = true;

                if (value < movePoints.Count - 1)
                    value++;
                else value = 0;

                FlipObject(movePoints[value].transform.position);

                if (isTeleport)
                {
                    isTeleport = false;
                    transform.position = movePoints[value].transform.position;
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, movePoints[value].transform.position, 3f * Time.deltaTime);

        }

        private void FlipObject(Vector2 target)
        {
            if(transform.position.x < target.x)
                transform.rotation = Quaternion.Euler(0, 180, 0);
            else if(transform.position.x > target.x)
                transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
