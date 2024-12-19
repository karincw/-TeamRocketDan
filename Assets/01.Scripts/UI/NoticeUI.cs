using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JSY
{
    public class NoticeUI : MonoSingleton<NoticeUI>
    {
        [SerializeField] private TextMeshProUGUI noticeText;
        private RectTransform rectTrm => transform as RectTransform;

        public void Notice(string description)
        {
            noticeText.text = description;
            Sequence seq = DOTween.Sequence();
            seq.Append(rectTrm.DOAnchorPosY(-250, 0.5f));
            seq.AppendInterval(0.5f);
            seq.Append(rectTrm.DOAnchorPosY(0, 0.5f));
        }
    }
}
