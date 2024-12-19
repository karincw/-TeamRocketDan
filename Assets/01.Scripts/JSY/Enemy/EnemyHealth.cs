using Karin.PoolingSystem;
using Leo.Entity.SO;
using Leo.Interface;
using Leo.Sound;
using UnityEngine;

namespace JSY
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private AttackText attackText;
        [SerializeField] private EnemyHealthBar healthBar;
        [SerializeField] private SoundObject _deadSound;
        public int HP { get; set; }
        public int defense { get; set; }
        private int maxHP;
        public int reward { get; set; }
        public bool IsDead => HP <= 0;
        
        private Enemy _owner;
        public void SetOwner(Enemy owner)
        {
            _owner = owner;
        }
        
        public void SetData(EnemySO enemySO)
        {
            HP = enemySO.maxHealth;
            defense = enemySO.defense;
            maxHP = enemySO.maxHealth;
            HP = Mathf.Clamp(HP, 0, enemySO.maxHealth);
            reward = enemySO.reward;
            healthBar.SetHealthBar((float)HP / maxHP);
        }
        
        public void TakeDamage(int damage)
        {
            int hitAmount = damage - Mathf.CeilToInt(defense * 0.5f);
            if (hitAmount < 0)
                hitAmount = 0;
            var text = Instantiate(attackText, transform.position, Quaternion.identity);
            
            HP -= hitAmount;
            healthBar.SetHealthBar((float)HP / maxHP);
            if (HP <= 0)
                Die();
            text.SetText(hitAmount.ToString());
        }

        public void Die()
        {
            Debug.Log("Die");
            if (_owner is not MiniBoss)
            {
                EnemyCountUI.Instance.UpdateCount(-1);
            }

            ResultUI.Instance.AddDeadEnemy();
            MoneyUI.Instance.ModifyMoney(reward);
            _deadSound.Play();
            PoolManager.Instance.Push(_owner);
        }
    }
}