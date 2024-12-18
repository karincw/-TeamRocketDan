using Leo.Damage;
using UnityEngine;

namespace JSY.Boss
{
    [CreateAssetMenu(fileName = "BossSkill", menuName = "SO/Enemy/Boss/Skill/BlasterSkill")]
    public class BlasterSkill : BossSkillSO
    {
        public Blaster blaster;
        public override void UseSkill(Transform target)
        {
            var blaster = Instantiate(this.blaster, _owner.transform);
            blaster.SetTarget(target);
            blaster.Shot();
        }
    }
}