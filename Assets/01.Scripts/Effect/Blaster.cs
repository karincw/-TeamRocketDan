using DG.Tweening;
using Mochi.Core;
using Mochi.Interface;
using UnityEngine;

namespace Mochi.Effect
{
    public class Blaster : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private GameObject _visual;
        [SerializeField] private float _duration = 2f;
        
        public void SetTarget(Transform target)
        {
            transform.right = target.position - transform.position;
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
                .OnComplete(() => _visual.SetActive(false));
            CameraManager.Instance.ShakeCamera(0.1f, _duration);
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