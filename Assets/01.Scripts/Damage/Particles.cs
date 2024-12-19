using System;
using Leo.Interface;
using UnityEngine;

namespace Leo.Damage
{
    public class Particles : MonoBehaviour, IEffectable, IColorChangeable
    {
        private static readonly int MainColor = Shader.PropertyToID("_MainColor");
        [SerializeField] private ParticleSystem _particleSystem;
        private DamageCaster _damageCaster;
        private Material _material;

        protected virtual void Awake()
        {
            _damageCaster = GetComponent<DamageCaster>();
            var renderer = _particleSystem.GetComponent<Renderer>();    
            _material = Instantiate(renderer.material);
            renderer.material = _material;
        }

        public void SetPos(Transform target)
        {
            transform.position = target.position;
        }

        public void Play()
        {
            _particleSystem.Play();
        }

        public DamageCaster GetDamageCaster()
        {
            return _damageCaster;
        }

        public void SetColor(Color color)
        {
            _material.SetColor(MainColor, color);
        }

        public void SetSize(float size)
        {
            transform.localScale = new Vector3(size, size, 1);
        }
    }
}