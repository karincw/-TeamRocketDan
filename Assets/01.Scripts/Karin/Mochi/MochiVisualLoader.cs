using System.Collections.Generic;
using UnityEngine;

namespace Karin
{
    public class MochiVisualLoader : MochiCompo
    {
        private SpriteRenderer _renderer;
        private SpriteRenderer _starRenderer;
        private SpriteRenderer _radiusRenderer;

        [SerializeField] private List<Sprite> starImage;
         
        protected override void Awake()
        {
            base.Awake();
            _renderer = transform.Find("Visual").GetComponent<SpriteRenderer>();
            _starRenderer = transform.Find("Star").GetComponent<SpriteRenderer>();
            _radiusRenderer = transform.Find("AttackRadiius").GetComponent<SpriteRenderer>();
        }

        public override void SetUp()
        {
            _renderer.sprite = _owner.MochiData.image;
            _starRenderer.sprite = starImage[(int)_owner.MochiData.ranking];
            _radiusRenderer.transform.localScale = Vector3.one * (_owner.MochiData.attackData.attackRange * 2 + 0.55f + _owner.MochiData.attackData.attackRange * 0.15f);
            SetAttackDistance(false);
        }

        public void SetAttackDistance(bool state)
        {
            _radiusRenderer.enabled = state;
        }
    }
}