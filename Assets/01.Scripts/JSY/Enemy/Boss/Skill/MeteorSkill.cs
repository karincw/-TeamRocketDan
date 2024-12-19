using Leo.Damage;
using UnityEngine;

namespace JSY.Boss
{
    [CreateAssetMenu(fileName = "BossSkill", menuName = "SO/Enemy/Boss/Skill/MeteorSkill")]
    public class MeteorSkill : BossSkillSO
    {
        public Meteor Meteor;
        public Vector2 minSpawnPos;
        public Vector2 maxSpawnPos;
        public override void UseSkill(Transform target)
        {
            var meteor = Instantiate(Meteor, GetRandomPosition(), Quaternion.identity);
            meteor.SetFallDir(target.position - meteor.transform.position);
        }
        
        private Vector2 GetRandomPosition()
        {
            return new Vector2(Random.Range(minSpawnPos.x, maxSpawnPos.x), Random.Range(minSpawnPos.y, maxSpawnPos.y));
        }
    }
}