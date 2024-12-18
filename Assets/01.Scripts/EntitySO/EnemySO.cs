using UnityEngine;

namespace Mochi.Entity.SO
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "SO/Enemy")]
    public class EnemySO : ScriptableObject
    {
        [Header("Enemy Info")]
        public int maxHealth;
        public int damage;
        public int defense;
        public float speed;
        public bool isBoss;
        
        [Header("Enemy Description")]
        public Sprite sprite;
    }
}

