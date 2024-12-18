using UnityEngine;
using UnityEngine.UIElements;

namespace Karin
{
    public class MochiAttacker : MochiCompo
    {
        private CircleCollider2D _collider;

        protected override void Awake()
        {
            base.Awake();
            _collider = GetComponent<CircleCollider2D>();
        }

        public override void SetUp()
        {
            _collider.radius = _owner.MochiData.attackRange;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            
        }

    }
}