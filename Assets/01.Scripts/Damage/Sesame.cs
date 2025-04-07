using System;
using Leo.Interface;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Leo.Damage
{
    public class Sesame : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _sr;
        [SerializeField] private SpriteRenderer _warning;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;
        private float _stunDuration;
        private float _curSpeed;
        private void Start()
        {
            StartCoroutine(WarningCoroutine());
            Destroy(gameObject, _lifeTime);
        }

        private IEnumerator WarningCoroutine()
        {
            _curSpeed = 0;
            yield return new WaitForSeconds(1f);
            _warning.gameObject.SetActive(false);
            _curSpeed = _speed;
        }

        public void SetDirection(Vector2 direction)
        {
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        }
        public void SetStunDuration(float stunDuration)
        {
            _stunDuration = stunDuration;
        }
        
        private void Update()
        {
            transform.Translate(Vector2.right * (_curSpeed * Time.deltaTime));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IStunable stun))
            {
                stun.Stun(_stunDuration);
                Destroy(gameObject);
            }
        }
    }
}