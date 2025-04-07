using DG.Tweening;
using System;
using UnityEngine;

namespace Leo.UI
{
    public class CutSceneCell : MonoBehaviour
    {
        [SerializeField] protected Vector3 _moveDir;
        [SerializeField] protected float _duration;
        [SerializeField] protected float _alpha = 1f;
        [SerializeField] protected Ease _ease;
        [SerializeField] protected CutSceneCell _nextCell;
        [SerializeField] protected CutSceneCell _nextCellTogether;

        private SpriteRenderer _spriteRenderer;
        protected virtual void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.DOFade(0, 0);
        }

        public virtual void Play(TweenCallback callback = null)
        {
            _spriteRenderer.DOFade(_alpha, _duration).SetEase(_ease).OnComplete(callback);
            transform.DOMove(transform.position + _moveDir, _duration).SetEase(_ease)
                .OnComplete(() =>
                {
                    if (_nextCell != null)
                    {
                        _nextCell.Play();
                        CutSceneManager.Instance._index++;
                    }
                });
            if (_nextCellTogether != null)
            {
                _nextCellTogether.Play();
                CutSceneManager.Instance._index++;
            }
        }
    }

}