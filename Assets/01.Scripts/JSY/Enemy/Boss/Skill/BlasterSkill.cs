using Mochi.Effect;
using UnityEngine;

namespace JSY.Boss
{
    
    [CreateAssetMenu(fileName = "BossSkill", menuName = "SO/Enemy/Boss/Skill/BlasterSkill")]
    public class BlasterSkill : BossSkillSO
    {
        public Blaster blaster;
        public override void UseSkill(Transform target)
        {
            blaster.SetTarget(target);
            blaster.Shot();
        }
    }
}