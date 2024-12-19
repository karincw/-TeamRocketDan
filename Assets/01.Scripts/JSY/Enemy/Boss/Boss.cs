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

        [Header("Pudding")] 
        [SerializeField] private GameObject _shild;
        [SerializeField] private bool _isPudding;
        
        public bool IsSkillUse { get; set; }


        protected override void Start()
        {
            base.Start();
            _bossSkill = Instantiate(_bossSkill);
            _bossSkill.SetOwner(this);
            transform.localScale = new Vector3(1.5f, 1.5f, 1f);
            StartCoroutine(FindMochi());
        }
        
        public override void ResetItem()
        {
            base.ResetItem();
            
            _bossSkill.SetOwner(this);
            StartCoroutine(FindMochi());
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
                if (_isPudding)
                    _shild.SetActive(false);
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
            if (_isPudding)
            {
                _shild.SetActive(true);
            }
            _bossSkill.UseSkill(target);
        }
    }
}