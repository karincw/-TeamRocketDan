using UnityEngine;
using UnityEngine.UIElements;

namespace Karin
{
    public class MochiAttacker : MonoBehaviour
    {
        private Mochi _owner;
        private CircleCollider2D _collider;

        private void Awake()
        {
            _owner = GetComponentInParent<Mochi>();
            _collider = GetComponent<CircleCollider2D>();
        }

        private void Start()
        {
            _collider.radius = _owner.MochiData.attackRange;
        }

    }
}