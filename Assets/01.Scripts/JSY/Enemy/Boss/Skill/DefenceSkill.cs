using UnityEngine;

namespace JSY.Boss
{
    public class DefenceSkill : BossSkillSO
    {
        private int _defense;
        public override void UseSkill(Transform target)
        {
            _defense = _owner.EnemyHealth.defense;
            _owner.EnemyHealth.defense += 10;
        }

        public override void ResetSkill()
        {
            _owner.EnemyHealth.defense = _defense;
        }
    }
}