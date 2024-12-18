using Mochi.Interface;
using UnityEngine;

namespace JSY.Boss
{
    public class Boss : MonoBehaviour
    {
        [SerializeField] private LayerMask _whatIsMochi;
        [SerializeField] private float _stunTime = 1f;
        [SerializeField] private BossSkillSO _bossSkill;
        
        private void TakeSkill(Transform target)
        {
            _bossSkill.UseSkill(target);
        }
    }
}