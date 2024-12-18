using JSY;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

            }
        }

        public override void SetUp()
        {
            _collider.radius = _owner.MochiData.attackData.attackRange;
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