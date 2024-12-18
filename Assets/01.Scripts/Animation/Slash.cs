using System;
using DG.Tweening;
using Leo.Damage;
using Leo.Interface;
using UnityEngine;

namespace Leo.Animation
{
    public class Slash : MonoBehaviour, IEffectable, IColorChangeable
    {
        [SerializeField] private GameObject _visual;
        [SerializeField] private TrailRenderer _trailRenderer;
        private Material _material;
        private void Awake()
        {
            _material = Instantiate(_trailRenderer.material);
            _trailRenderer.material = _material;
        }

        public void SetColor(Color color)
        {
            _material.SetColor("_MainColor", color);
        }

        [ContextMenu("StartSlash")]
        public void StartSlash()
        {
            transform.DOKill();
            transform.DORotate(new Vector3(0, 0, -90), 0f);

            Sequence sequence = DOTween.Sequence()
                .Append(transform.DORotate(new Vector3(0, 0, -90), 0.2f))
                .Join(transform.DORotate(new Vector3(0, 0, 0), 0.2f))
                .Join(transform.DORotate(new Vector3(0, 0, 90), 0.2f))
                .SetEase(Ease.Linear)
                .OnComplete(() => Destroy(gameObject));

        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }
#endif
        public void SetPos(Transform target)
        {
            transform.position = target.position;
        }

        public void Play()
        {
            StartSlash();
        }

        public DamageCaster GetDamageCaster()
        {
            return null;
        }
    }
}