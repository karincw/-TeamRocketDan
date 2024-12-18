using Leo.Entity.SO;
using Leo.Interface;
using UnityEngine;

namespace JSY
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        public int HP { get; set; }
        public int defense { get; set; }
        
        public void SetData(EnemySO enemySO)
        {
            HP = enemySO.maxHealth;
            defense = enemySO.defense;
        }
        
        public void TakeDamage(int damage)
        {
            Debug.Log("Hit");
            HP -= damage - (int)(defense * 0.5f);
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}