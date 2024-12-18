using System.Collections;
using Leo.Damage;
using UnityEngine;

namespace JSY.Boss
{
    [CreateAssetMenu(fileName = "BossSkill", menuName = "SO/Enemy/Boss/Skill/SesameShotSkill")]
    public class SesameShotSkill : BossSkillSO
    {
        public Sesame sesame;
        public int count;
        public override void UseSkill(Transform target)
        {
            _owner.StartCoroutine(Shot(target));
        }

        private IEnumerator Shot(Transform target)
        {
            for (int i = 0; i < count; i++)
            {
                var sesames = Instantiate(sesame, _owner.transform.position, Quaternion.identity);
                sesames.SetDirection(target.position - _owner.transform.position);
                sesames.SetStunDuration(_owner._stunTime);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
