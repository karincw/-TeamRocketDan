using System.Collections;
using UnityEngine;

namespace JSY.Boss
{
    public class Boss : Enemy
    {
        [SerializeField] private LayerMask _whatIsMochi;
        [SerializeField] public float _stunTime = 1f;
        [SerializeField] private BossSkillSO _bossSkill;
        [SerializeField] private bool isStop;
        private Collider2D[] _colliders = new Collider2D[1];
        public bool IsSkillUse { get; set; }


        protected override void Start()
        {
            base.Start();
            if (_bossSkill != null)
            {
                _bossSkill = Instantiate(_bossSkill);
                _bossSkill.SetOwner(this);
                StartCoroutine(FindMochi());
            }
            transform.localScale = new Vector3(1.5f, 1.5f, 1f);
        }

        public override void ResetItem()
        {
            base.ResetItem();
            if (_bossSkill != null)
            {
                _bossSkill.SetOwner(this);
                StartCoroutine(FindMochi());
            }
        }

        protected override void Update()
        {
            if (!IsSkillUse)
                base.Update();
        }

        private IEnumerator FindMochi()
        {
            while (true)
            {
                FindTarget();
                yield return new WaitForSeconds(5f);
                _colliders[0] = null;
                _bossSkill.ResetSkill();
                IsSkillUse = false;
                yield return new WaitForSeconds(2f);

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
            if (isStop)
                IsSkillUse = true;
            if (_bossSkill != null)
            {
                _bossSkill.UseSkill(target);
            }
        }
    }
}