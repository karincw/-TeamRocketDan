using UnityEngine;

namespace Karin
{
    public enum TowerRanking : int
    {
        zero, one, two, three, four, five
    }

    [CreateAssetMenu(menuName = "SO/Tower/MochiSO")]
    public class MochiDataSO : ScriptableObject
    {
        public int damage;
        public int attackRange;
        public int attackCooldown;
        public Sprite image;
        public TowerRanking ranking;
    }
}