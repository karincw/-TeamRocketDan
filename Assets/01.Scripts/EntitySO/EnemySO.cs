using UnityEngine;

namespace Mochi.Entity.SO
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "SO/Enemy")]
    public class EnemySO : ScriptableObject
    {
        public Sprite sprite;
        public int maxHealth;
        public int damage;
        public int defense;
        public float speed;
    }
}

