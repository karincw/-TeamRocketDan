using UnityEngine;
using DG.Tweening;

namespace Leo.Animation
{
    public class BounceAnimation : MonoBehaviour
    {
        [SerializeField] private float _speed = 0.5f;
        [SerializeField] private float _scale = 1.2f;
        [SerializeField] private Ease _ease = Ease.InQuad;
        
        private void Start()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOScaleX(_scale, _speed / 2));
            sequence.Append(transform.DOScaleX(1f, _speed / 2));
            sequence.Join(transform.DOScaleY(_scale, _speed / 2));
            sequence.Append(transform.DOScaleY(1f, _speed / 2));
            sequence.SetLoops(-1);
            sequence.SetEase(_ease);
            sequence.Play();
        }
    }
}