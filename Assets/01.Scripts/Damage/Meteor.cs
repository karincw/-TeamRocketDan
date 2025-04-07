using System;
using Leo.Interface;
using System.Collections;
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
        [SerializeField] private Transform _warningCircle;
        
        private float _curSpeed;
        
        public void SetFallDir(Vector2 dir)
        {
            _fallDir = dir;
        }

        public void SetTarget(Vector3 target)
        {
            _warningCircle.position = target;
            StartCoroutine(WarningCoroutine());
        }

        private IEnumerator WarningCoroutine()
        {
            _warningCircle.gameObject.SetActive(true);
            _curSpeed = 0;
            yield return new WaitForSeconds(1f);
            _warningCircle.gameObject.SetActive(false);
            _curSpeed = _fallSpeed;
        }

        private void Update()
        {
            transform.Translate(_fallDir * (_curSpeed * Time.deltaTime));
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