using UnityEngine;

namespace Karin
{
    public enum TowerRanking
    {
        zero, one, two, three, four, five
    }

    [CreateAssetMenu(menuName = "SO/Tower/MochiSO")]
    public class MochiSO : ScriptableObject
    {
        public int damage;
        public int attackRange;
        public int attackCooldown;
        [TextArea(1, 3)]
        public string description;
    }
}