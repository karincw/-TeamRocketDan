using System;
using Leo.Interface;
using UnityEngine;

namespace Leo.Damage
{
    public class Particles : MonoBehaviour, IEffectable, IColorChangeable
    {
        [SerializeField] private ParticleSystem _particleSystem;
        private DamageCaster _damageCaster;
        private Material _material;

        private void Awake()
        {
            _damageCaster = GetComponent<DamageCaster>();
            _material = Instantiate(_particleSystem.GetComponent<Renderer>().material);
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
            _material.color = color;
        }
    }
}