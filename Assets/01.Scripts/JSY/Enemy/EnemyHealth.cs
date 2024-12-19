using Leo.Entity.SO;
using Leo.Interface;
using UnityEngine;

namespace JSY
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private AttackText attackText;
        public int HP { get; set; }
        public int defense { get; set; }
        public int reward { get; set; }

        
        public void SetData(EnemySO enemySO)
        {
            HP = enemySO.maxHealth;
            defense = enemySO.defense;
            HP = Mathf.Clamp(HP, 0, enemySO.maxHealth);
            reward = enemySO.reward;
        }
        
        public void TakeDamage(int damage)
        {
            int hitAmount = damage - Mathf.CeilToInt(defense * 0.5f);
            if (hitAmount < 0)
                hitAmount = 0;
            var text = Instantiate(attackText, transform.position, Quaternion.identity);
            text.SetText(hitAmount.ToString());
            HP -= hitAmount;
            if (HP <= 0)
                Die();
        }

        public void Die()
        {
            EnemyCountUI.Instance.UpdateCount(-1);
            MoneyUI.Instance.ModifyMoney(reward);
            Destroy(gameObject);
        }
    }
}