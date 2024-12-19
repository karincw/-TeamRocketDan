using DG.Tweening;
using System;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

namespace Karin.DialogSystem
{
    public class DialogCanvas : MonoBehaviour
    {
        [Header("OnDialog")]
        [SerializeField] private Canvas _dialogCanvasPrefab;
        public static Canvas dialogCanvas;
        //public Canvas CharactorHeadCanvas => _characterHeadCanvas;

        //[Space]
        //[Header("OnHead")]
        //[SerializeField] private Canvas _characterHeadCanvas;

        private CanvasGroup _dialogCanvasGroup;
        private CanvasGroup _headCanvasGroup;
        private TMP_Text _headText;
        private TMP_Text _dialogText;

        private void Awake()
        {
            if (dialogCanvas == null)
            {
                dialogCanvas = Instantiate(_dialogCanvasPrefab);
            }
            _dialogCanvasGroup = dialogCanvas.GetComponent<CanvasGroup>();
            _dialogText = dialogCanvas.GetComponentInChildren<TMP_Text>();
            //var spoken = _characterHeadCanvas.transform.Find("Spoken");
            //_headCanvasGroup = spoken.GetComponent<CanvasGroup>();
            //_headText = spoken.GetComponentInChildren<TMP_Text>();

            //_headCanvasGroup.alpha = 0f;
            _dialogCanvasGroup.alpha = 0f;
        }

        public void SetHeadText(string text, float textTime, bool fade = true, float fadeTime = 1.2f)
        {
            _headCanvasGroup.alpha = 1;
            _headText.text = text;

            Sequence seq = DOTween.Sequence()
                .Append(DOTween.To(() => _headText.text = "", t => _headText.text = t, text, textTime).SetEase(Ease.Linear)).SetId(32)
                .AppendInterval(0.3f);

            if (fade)
                seq.Append(DOTween.To(() => _headCanvasGroup.alpha = 1, a => _headCanvasGroup.alpha = a, 0f, fadeTime));
        }
        public void SetDialogText(string text, float textTime, bool fade = true, float fadeTime = 1.2f)
        {
            _dialogCanvasGroup.alpha = 1;
            _dialogText.text = text;

            Sequence seq = DOTween.Sequence()
                .Append(DOTween.To(() => _dialogText.text = "", t => _dialogText.text = t, text, textTime).SetEase(Ease.Linear)).SetId(32)
                .AppendInterval(0.3f);

            if (fade)
                seq.Append(DOTween.To(() => _dialogCanvasGroup.alpha = 1, a => _dialogCanvasGroup.alpha = a, 0f, fadeTime));
        }
        public TMP_Text GetCurrentText(DialogPosition position)
        {
            if (position == DialogPosition.OnHead)
            {
                return _headText;
            }
            else if (position == DialogPosition.UnderBar)
            {
                return _dialogText;
            }
            else
            {
                return null;
            }
        }
    }
}
