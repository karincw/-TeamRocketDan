using System;
using Leo.Interface;
using UnityEngine;

namespace Leo.Damage
{
    public class Particles : MonoBehaviour, IEffectable
    {
        [SerializeField] private ParticleSystem _particleSystem;
        private DamageCaster _damageCaster;

        private void Awake()
        {
            _damageCaster = GetComponent<DamageCaster>();
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
    }
}