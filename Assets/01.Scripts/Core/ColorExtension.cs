using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

namespace Leo.Core
{
    public static class ColorExtension
    {
        public static void DOColor(this ColorParameter color, Color targetColor, float duration, TweenCallback onComplete = null)
        {
            DOTween.To(() => color.value, x => color.value = x, targetColor, duration)
                .SetTarget(color).OnComplete(onComplete);
        }
    }
}