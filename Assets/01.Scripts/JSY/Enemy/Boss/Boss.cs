using System;
using System.Collections;
using Leo.Interface;
using UnityEngine;

namespace JSY.Boss
{
    public class Boss : Enemy
    {
        [SerializeField] private LayerMask _whatIsMochi;
        [SerializeField] private float _stunTime = 1f;
        [SerializeField] private BossSkillSO _bossSkill;
        
        private Collider2D[] _colliders = new Collider2D[1];


        private void Start()
        {
            _bossSkill = Instantiate(_bossSkill);
            _bossSkill.SetOwner(this);
            StartCoroutine(FindMochi());
        }

        private IEnumerator FindMochi()
        {
            while (true)
            {
                FindTarget();
                yield return new WaitForSeconds(1f);
                _colliders[0] = null;
                yield return null;
            }
        }

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