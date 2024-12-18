using UnityEngine;

namespace Karin
{
    public enum TowerRanking : int
    {
        zero = 0, one, two, three, four, five
    }

    [CreateAssetMenu(menuName = "SO/Tower/MochiSO")]
    public class MochiDataSO : ScriptableObject
    {
        public Sprite image;
        public TowerRanking ranking;
        public AttackData attackData;
    }

    [System.Serializable]
    public struct AttackData
    {
        public int damage;
        public int attackRange;
        public int attackCooldown;
    }

}