using UnityEngine;

namespace Karin
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MochiVisualLoader : MochiCompo
    {
        private SpriteRenderer _renderer;

        protected override void Awake()
        {
            base.Awake();
            _renderer = GetComponent<SpriteRenderer>();
        }

        public override void SetUp()
        {
            _renderer.sprite = _owner.MochiData.image;
        }
    }
}