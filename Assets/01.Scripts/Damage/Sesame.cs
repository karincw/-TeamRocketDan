using System;
using Leo.Interface;
using UnityEngine;

namespace Leo.Damage
{
    public class Sesame : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _sr;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;
        private float _stunDuration;
        private void Start()
        {
            Destroy(gameObject, _lifeTime);
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
            transform.Translate(Vector2.right * (_speed * Time.deltaTime));
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