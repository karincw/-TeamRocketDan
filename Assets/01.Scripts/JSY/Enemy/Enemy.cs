using Karin.PoolingSystem;
using System.Collections.Generic;
using Leo.Entity.SO;
using UnityEngine;

namespace JSY
{
    public class Enemy : MonoBehaviour, IPoolable
    {
        [SerializeField] private List<MovePoint> movePoints = new List<MovePoint>();
        [SerializeField] protected EnemySO _enemySO;
        private int value;
        public EnemyHealth EnemyHealth { get; private set; }
        protected SpriteRenderer _spriteCompo;
        private bool isTeleport;
        public void SetMovePoints(List<MovePoint> movePoints) => this.movePoints = movePoints;

        private void Awake()
        {
            EnemyHealth = GetComponent<EnemyHealth>();
            _spriteCompo = GetComponentInChildren<SpriteRenderer>();
            EnemyHealth.SetOwner(this);
        }

        protected virtual void Start()
        {
            Init();
        }

        private void Init()
        {
            EnemyHealth.SetData(_enemySO);
            _spriteCompo.sprite = _enemySO.sprite;
        }

        protected virtual void Update()
        {
            if(Vector2.Distance(transform.position, movePoints[value].transform.position) <= 0.01f)
            {
                if (movePoints[value].isTeleport)
                    isTeleport = true;

                if (value < movePoints.Count - 1)
                    value++;
                else value = 0;

                FlipObject(movePoints[value].transform.position);

                if (isTeleport)
                {
                    isTeleport = false;
                    transform.position = movePoints[value].transform.position;
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, movePoints[value].transform.position, _enemySO.speed * Time.deltaTime);

        }

        private void FlipObject(Vector2 target)
        {
            if(transform.position.x < target.x)
                _spriteCompo.flipX = true;
            else if(transform.position.x > target.x)
                _spriteCompo.flipX = false;
        }

        #region Pool
        [field:SerializeField]public PoolingType type { get; set; }
        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public virtual void ResetItem()
        {
            Init();
        }

        public void OnPush()
        {
            Init();
            transform.parent = PoolManager.Instance.transform;
        }

        #endregion
    }
}
