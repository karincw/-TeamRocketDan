using DG.Tweening;
using Leo.Core;
using Leo.Interface;
using UnityEngine;

namespace Leo.Damage
{
    public class Blaster : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private GameObject _visual;
        [SerializeField] private GameObject _visual2;
        [SerializeField] private float _duration = 2f;
        
        public void SetPos(Transform target)
        {
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        
        public void Play()
        {
            _visual2.SetActive(true);
            _visual.SetActive(true);
            _visual.transform.localScale = new Vector3(0, 1, 1);
            _visual2.transform.localScale = new Vector3(0, 1, 1);
            _particleSystem.Play();
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_visual2.transform.DOScaleX(50, 1f))
                .AppendInterval(1f)
                .AppendCallback(() =>
                {
                    _visual2.SetActive(false);
                    CameraManager.Instance.ShakeCamera(0.03f, _duration);
                })
                .Append(_visual.transform.DOScaleX(50, 1f))
                .AppendInterval(_duration)
                .OnComplete(() => Destroy(gameObject));
            
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