using Leo.Interface;
using UnityEngine;

namespace Leo.Damage
{
    public class StarLite : DamageCaster, IColorChangeable
    {
        [field:SerializeField] public SpriteRenderer SrCompo { get; private set; }
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private TrailRenderer _trailRenderer;
        private Material _material;
        private Material _material2;
        private void Awake()
        {
            _material = Instantiate(_trailRenderer.material);
            _trailRenderer.material = _material;
            var renderer = _particleSystem.GetComponent<Renderer>();
            _material2 = Instantiate(renderer.material);
            renderer.material = _material2;
        }

        public void SetImage(Sprite sprite)
        {
            SrCompo.sprite = sprite;
        }

        public void SetColor(Color color)
        {
            _material.color = color;
            _material.SetColor("_EmissionColor", color);
            _material2.color = color;
            _material2.SetColor("_EmissionColor", color * 16);
        }
    }

}
