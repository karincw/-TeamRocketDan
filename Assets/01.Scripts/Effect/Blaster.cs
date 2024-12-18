using DG.Tweening;
using Leo.Core;
using Leo.Interface;
using UnityEngine;

namespace Leo.Effect
{
    public class Blaster : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private GameObject _visual;
        [SerializeField] private float _duration = 2f;
        
        public void SetTarget(Transform target)
        {
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        
        public void Shot()
        {
            _visual.SetActive(true);
            transform.DOComplete();
            transform.localScale = new Vector3(0, 1, 1);
            _particleSystem.Play();
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOScaleX(5, 0.5f))
                .AppendInterval(_duration)
                .OnComplete(() => Destroy(gameObject));
            CameraManager.Instance.ShakeCamera(0.05f, _duration);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IStunable stun))
            {
                stun.Stun(5);
            }
        }
    }
}