using Leo.Damage;
using UnityEngine;

namespace JSY.Boss
{
    [CreateAssetMenu(fileName = "BossSkill", menuName = "SO/Enemy/Boss/Skill/MeteorSkill")]
    public class MeteorSkill : BossSkillSO
    {
        public Meteor Meteor;
        public Vector2 spawnPos;
        public override void UseSkill(Transform target)
        {
            Instantiate(Meteor, spawnPos, Quaternion.identity);
        }
    }
}