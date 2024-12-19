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
        private List<Enemy> _enemies = new List<Enemy>();
        private float lastAttacktime;

        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<CircleCollider2D>();
            lastAttacktime = Time.time;
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
            if (attackData.isStarlite || _enemies.Count < 1 || attackData.attackEffect == null) return;

            if (Time.time - lastAttacktime >= attackData.attackCooldown)
            {
                var attackEffect = Instantiate(attackData.attackEffect, _enemies[0].transform.position, Quaternion.identity);
                if (attackEffect is IEffectable effect)
                {
                    if (attackEffect is IColorChangeable colorChange)
                    {
                        colorChange.SetColor(attackData.attackColor);
                    }
                    if(attackEffect is ISizeChangeable sizeChange)
                    {
                        sizeChange.SetSize(attackData.size);
                    }

                    var damageCaster = effect.GetDamageCaster();
                    if (damageCaster)
                    {
                        damageCaster.SetDamage(attackData.damage);
                    }
                    else
                    {
                        _enemies[0].EnemyHealth.TakeDamage(attackData.damage);
                    }
                    effect.Play();
                }
                lastAttacktime = Time.time;
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
                    spin.SetData(attackData.attackRange, attackData.count, 1);
                    StarLite sl = (spin.GetDamageCaster() as StarLite);
                    sl.SetImage(attackData.starLiteImage);
                    sl.SetDamage(attackData.damage);
                    spin.Play();
                    spin.SetColor(attackData.attackColor);
                }
            }

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                _enemies.Add(collision.gameObject.GetComponent<Enemy>());
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                _enemies.Remove(collision.gameObject.GetComponent<Enemy>());
            }
        }

    }
}