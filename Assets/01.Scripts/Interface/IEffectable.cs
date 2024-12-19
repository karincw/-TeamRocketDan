using Leo.Damage;
using UnityEngine;

namespace Leo.Interface
{
    public interface IEffectable : ISizeChangeable
    {
        public void SetPos(Transform target);
        public void Play();
        public DamageCaster GetDamageCaster();
    }
}