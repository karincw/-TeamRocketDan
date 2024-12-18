using UnityEngine;

namespace Leo.Interface
{
    public interface IEffectable
    {
        public void SetPos(Transform target);
        public void Play();
    }
}