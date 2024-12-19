using Leo.Interface;
using UnityEngine;

namespace Leo.Damage
{
    public class DamageCaster : MonoBehaviour
    {
        [SerializeField] private int _damage;
        public bool canAttack = true;
        
        public void SetDamage(int damage)
        {
            _damage = damage;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (canAttack && other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);
            }
        }
    }
}