using UnityEngine;

namespace Karin
{
    public abstract class MochiCompo : MonoBehaviour
    {
        protected Mochi _owner;

        protected virtual void Awake()
        {
            _owner = GetComponentInParent<Mochi>();
        }

        public abstract void SetUp();
        public virtual void Release() { }
    }
}