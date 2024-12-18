using Leo.Interface;
using UnityEngine;

namespace JSY.Boss
{
    public class Boss : MonoBehaviour
    {
        [SerializeField] private LayerMask _whatIsMochi;
        [SerializeField] private float _stunTime = 1f;
        [SerializeField] private BossSkillSO _bossSkill;
        
        private Collider2D[] _colliders = new Collider2D[10];

        private void FindTarget()
        {
            int count = Physics2D.OverlapCircleNonAlloc(transform.position, 10f, _colliders, _whatIsMochi);
            if (count > 0)
            {
                TakeSkill(_colliders[0].transform);
            }
        }
        
        private void TakeSkill(Transform target)
        {
            _bossSkill.UseSkill(target);
        }
    }
}