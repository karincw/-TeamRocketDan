﻿using System;
using UnityEngine;

namespace Leo.Damage
{
    public class CircleSpinAttacker : MonoBehaviour
    {
        [SerializeField] private DamageCaster damageCaster;
        [SerializeField] private int _count;
        [SerializeField] private float _distance;

        private void Start()
        {
            for (int i = 0; i < _count; i++)
            {
                var position = new Vector2(
                    transform.position.x + _distance * Mathf.Cos(2 * Mathf.PI / _count * i),
                    transform.position.y + _distance * Mathf.Sin(2 * Mathf.PI / _count * i));
                
                    Instantiate(
                        damageCaster,
                        position,
                        Quaternion.identity,
                        transform);
            }
        }

        private void Update()
        {
            SpinAttack();
        }

        private void SpinAttack()
        {
            transform.Rotate(0, 0, 360 * Time.deltaTime);
        }
    }
}