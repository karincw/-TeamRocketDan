using DG.Tweening;
using System;
using UnityEngine;

namespace Leo.UI
{
    public class CutSceneUI : CutSceneCell
    {
        private CanvasGroup _canvasGroup;

        protected override void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.DOFade(0, 0);
        }

        public override void Play(TweenCallback callback = null)
        {
            _canvasGroup.DOFade(_alpha, _duration).SetEase(_ease).OnComplete(callback);
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