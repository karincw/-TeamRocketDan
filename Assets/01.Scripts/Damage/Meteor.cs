using System;
using Leo.Interface;
using UnityEngine;

namespace Leo.Damage
{
    public class Meteor : MonoBehaviour
    {
        [SerializeField] private Transform _visual;
        [SerializeField] private float _rotSpeed = 10f;
        [SerializeField] private float _fallSpeed = 5f;
        [SerializeField] private Vector2 _fallDir;
        [SerializeField] private ParticleSystem _explodeParticle;

        private void Update()
        {
            transform.Translate(_fallDir * (_fallSpeed * Time.deltaTime));
            _visual.Rotate(Vector3.forward, _rotSpeed * Time.deltaTime);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IStunable stun))
            {
                Instantiate(_explodeParticle, transform.position, Quaternion.identity);
                stun.Stun(5);
                Destroy(gameObject);
            }
        }
    }
}