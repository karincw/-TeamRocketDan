using UnityEngine;

namespace JSY.Boss
{
    [CreateAssetMenu(fileName = "BossSkill", menuName = "SO/Enemy/Boss/Skill/DefenceSkill")]
    public class DefenceSkill : BossSkillSO
    {
        public int UpgradeDefense = 10;
        public override void UseSkill(Transform target)
        {
            _owner.EnemyHealth.defense += UpgradeDefense;
        }

        public override void ResetSkill()
        {
            _owner.EnemyHealth.defense -= UpgradeDefense;
        }
    }
}