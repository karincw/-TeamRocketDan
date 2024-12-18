using UnityEngine;

namespace Karin
{
    public abstract class DragAndDropObject : MonoBehaviour
    {
        [SerializeField] protected Vector2 vaildPosition;
        [SerializeField] protected Collider2D _collisionCollider;

        public void ColliderEnable(bool state)
        {
            _collisionCollider.enabled = state;
        }
    }
}
