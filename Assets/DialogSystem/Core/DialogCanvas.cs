using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Karin.DialogSystem
{
    public class DialogCanvas : MonoBehaviour
    {
        [Header("OnDialog")]
        public TMP_Text dialogText;
        public CanvasGroup _dialogCanvasGroup;

        private void Awake()
        {
            _dialogCanvasGroup.alpha = 0;
            _dialogCanvasGroup.blocksRaycasts = true;
        }
        public void SetDialogText(string text, float textTime, bool fade = true, float fadeTime = 1.2f)
        {
            _dialogCanvasGroup.alpha = 1;
            dialogText.text = text;

            Sequence seq = DOTween.Sequence()
                .Append(DOTween.To(() => dialogText.text = "", t => dialogText.text = t, text, textTime).SetEase(Ease.Linear)).SetId(32)
                .AppendInterval(0.3f);

            if (fade)
            {
                seq.Append(DOTween.To(() => _dialogCanvasGroup.alpha = 1, a => _dialogCanvasGroup.alpha = a, 0f, fadeTime));
            }
            seq.JoinCallback(() => _dialogCanvasGroup.blocksRaycasts = !fade);
        }
    }
}
