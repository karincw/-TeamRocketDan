using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JSY
{
    public class NoticeUI : MonoBehaviour
    {
        [SerializeField] private RectTransform noticeRectTrm;
        [SerializeField] private TextMeshProUGUI noticeText;
        
        public void Notice(string description)
        {
            noticeText.text = description;
            Sequence seq = DOTween.Sequence();
            seq.Append(noticeRectTrm.DOAnchorPosY(-250, 0.5f));
            seq.AppendInterval(0.5f);
            seq.Append(noticeRectTrm.DOAnchorPosY(0, 0.5f));
        }
    }
}
