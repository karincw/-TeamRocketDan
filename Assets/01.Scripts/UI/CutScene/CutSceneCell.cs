using DG.Tweening;
using System;
using UnityEngine;

namespace Leo.UI
{
    public class CutSceneCell : MonoBehaviour
    {
        [SerializeField] private Vector3 _moveDir;
        [SerializeField] private float _duration;
        [SerializeField] private float _alpha = 1f;
        [SerializeField] private Ease _ease;
        [SerializeField] private CutSceneCell _nextCell;
        [SerializeField] private CutSceneCell _nextCellTogether;

        private SpriteRenderer _spriteRenderer;
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.DOFade(0, 0);
        }
        
        public void Play(TweenCallback callback = null)
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