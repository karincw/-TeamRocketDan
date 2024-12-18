using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private RectTransform popupUI;
    [SerializeField] private CanvasGroup popupCanvasGroup;

    private bool isOpen = false;
    private bool isAnimating = false;

    float durationTime = 0.2f;

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasReleasedThisFrame)
        {
            SettingPanel();
        }
    }

    public void SettingPanel()
    {
        if (isAnimating) return;
        isAnimating = true;

        isOpen = !isOpen;

        if (isOpen)
            ActiveUI(true);

        float fadeValue = isOpen ? 0.6f : 0f;
        float sizeValue = isOpen ? 1f : 0f;

        Sequence seq = DOTween.Sequence();
        seq.Append(background.DOFade(fadeValue, durationTime));
        seq.Join(popupUI.DOScale(sizeValue, durationTime));
        seq.OnComplete(() =>
        {
            popupCanvasGroup.blocksRaycasts = isOpen;
            isAnimating = false;
            if (!isOpen)
                ActiveUI(false);
        });
    }

    private void ActiveUI(bool isTrue)
    {
        background.gameObject.SetActive(isTrue);
        popupUI.gameObject.SetActive(isTrue);
    }
}
