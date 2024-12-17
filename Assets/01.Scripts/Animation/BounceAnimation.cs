using UnityEngine;
using DG.Tweening;

namespace Mochi.Animation
{
    public class BounceAnimation : MonoBehaviour
    {
        [SerializeField] private float _duration = 0.5f;
        
        private void Start()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOScaleX(1.2f, _duration / 2));
            sequence.Append(transform.DOScaleX(1f, _duration / 2));
            sequence.Join(transform.DOScaleY(1.2f, _duration / 2));
            sequence.Append(transform.DOScaleY(1f, _duration / 2));
            sequence.SetLoops(-1);
            sequence.SetEase(Ease.InQuad);
            sequence.Play();
        }
    }
}