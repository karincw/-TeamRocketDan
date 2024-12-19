using UnityEngine;
using DG.Tweening;
using System;

namespace Leo.Animation
{
    public class BounceAnimation : MonoBehaviour
    {
        [SerializeField] private float _speed = 0.5f;
        [SerializeField] private float _scale = 1.2f;
        [SerializeField] private Ease _ease = Ease.InQuad;
        
        private Sequence _sequence;
        
        private void Start()
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DOScaleX(_scale, _speed / 2));
            _sequence.Append(transform.DOScaleX(1f, _speed / 2));
            _sequence.Join(transform.DOScaleY(_scale, _speed / 2));
            _sequence.Append(transform.DOScaleY(1f, _speed / 2));
            _sequence.SetLoops(-1);
            _sequence.SetEase(_ease);
            _sequence.Play();
        }
        
        public void Stop()
        {
            _sequence.Kill();
        }
        
        public void Play()
        {
            _sequence.Play();
        }

        private void OnDestroy()
        {
            DOTween.Kill(transform);
        }
    }
}