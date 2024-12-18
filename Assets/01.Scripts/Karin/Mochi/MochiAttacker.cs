using DG.Tweening;
using JSY;
using Leo.Damage;
using Leo.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace Karin
{
    public class MochiAttacker : MochiCompo
    {
        private CircleCollider2D _collider;
        private List<Enemy> enemies = new();

        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<CircleCollider2D>();
        }

        private void Update()
        {
            if (_owner.CanAttack)
            {
                Attack();
            }
        }

        public void Attack()
        {
            var attackData = _owner.MochiData.attackData;
            if (attackData.isStarlite || enemies.Count < 0) return;

            var attackEffect = Instantiate(attackData.attackEffect, enemies[0].transform.position, Quaternion.identity);
            if(attackEffect is IEffectable effect)
            {
                effect.GetDamageCaster().SetDamage(attackData.damage);
                effect.Play();
            }

        }

        public override void SetUp()
        {
            _collider.radius = _owner.MochiData.attackData.attackRange;

            var attackData = _owner.MochiData.attackData;
            if (attackData.isStarlite)
            {
                var effect = Instantiate(attackData.attackEffect, transform);
                if (effect is CircleSpinAttacker spin)
                {
                    spin.SetData(attackData.attackRange, attackData.count);
                    spin.Play();
                }
            }

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                enemies.Add(collision.gameObject.GetComponent<Enemy>());
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                enemies.Remove(collision.gameObject.GetComponent<Enemy>());
            }
        }

    }
}