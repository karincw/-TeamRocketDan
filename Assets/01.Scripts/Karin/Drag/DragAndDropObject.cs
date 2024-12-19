using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Karin
{
    public abstract class DragAndDropObject : MonoBehaviour
    {
        [SerializeField] protected Vector2 vaildPosition;
        [SerializeField] protected Collider2D _collisionCollider;

        [HideInInspector] public bool isDrag;
        private List<Collider2D> results = new();

        public void ColliderTrigger(bool state)
        {
            _collisionCollider.isTrigger = state;
        }
        public virtual void ShowRadius(bool state)
        {
        }

        protected virtual void Update()
        {
            results.Clear();
            _collisionCollider.Overlap(results);
            if (results.Count(n => n.CompareTag("Platform")) > 0)
            {
                vaildPosition = transform.position;
            }
        }

        public void SetVailPos(Vector3 pos)
        {
            vaildPosition = pos;
        }

        public void VaildCheck()
        {
            Vector2 v = (Vector2)transform.position - vaildPosition;
            bool vaild = !(v.magnitude > 0.5f);
            if (!vaild)
            {
                transform.position = vaildPosition;
            }
        }
    }
}
