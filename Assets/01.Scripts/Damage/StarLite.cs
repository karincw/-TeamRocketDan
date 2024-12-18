using Leo.Interface;
using UnityEngine;

namespace Leo.Damage
{
    public class StarLite : DamageCaster, IColorChangeable
    {
        [field:SerializeField] public SpriteRenderer SrCompo { get; private set; }
        [SerializeField] private TrailRenderer _trailRenderer;
        private Material _material;
        private void Awake()
        {
            _material = Instantiate(_trailRenderer.material);
            _trailRenderer.material = _material;
        }

        public void SetImage(Sprite sprite)
        {
            SrCompo.sprite = sprite;
        }

        public void SetColor(Color color)
        {
            _material.color = color;
        }
    }

}
