using UnityEngine;

namespace Leo.Entity.SO
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "SO/Enemy")]
    public class EnemySO : ScriptableObject
    {
        [Header("Enemy Info")]
        public int maxHealth;
        public int damage;
        public int defense;
        public float speed;
        public int reward;
        
        [Header("Enemy Description")]
        public Sprite sprite;
    }
}

