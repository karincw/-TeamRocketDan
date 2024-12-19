using Leo.Interface;
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

        private void OnValidate()
        {
            if(attackData.attackEffect is not IEffectable)
            {
                attackData.attackEffect = null;
            }
        }
    }

    [System.Serializable]
    public struct AttackData
    {
        public int damage;
        public int attackRange;
        public int attackCooldown;
        public MonoBehaviour attackEffect;
        [ColorUsage(showAlpha:true, hdr:true)]public Color attackColor;

        [Space, Header("StarLite")]
        public bool isStarlite;
        public int count;
        public Sprite starLiteImage;
        public float speedTimeMultiply;

        [Space, Header("Explosion")]
        public float size;
    }

}