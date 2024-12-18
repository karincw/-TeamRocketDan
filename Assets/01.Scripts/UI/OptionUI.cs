using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private RectTransform popupUI;
    [SerializeField] private CanvasGroup popupCanvasGroup;

    private Slider bgmSlider, sfxSlider;
    private Button infoButton, exitButton;

    private bool isOpen = false;
    private bool isAnimating = false;

    float durationTime = 0.2f;

    private void Awake()
    {
        Transform panel = transform.GetChild(1);
        bgmSlider = panel.Find("BGMSlider").GetComponent<Slider>();
        sfxSlider = panel.Find("SFXSlider").GetComponent<Slider>();
        infoButton = panel.Find("InfoBtn").GetComponent<Button>();
        exitButton = panel.Find("ExitBtn").GetComponent<Button>();

        infoButton.onClick.AddListener(HandleInfoButton);
        exitButton.onClick.AddListener(HandleExitButton);
    }

    private void HandleInfoButton()
    {
        throw new NotImplementedException();
    }

    private void HandleExitButton()
    {
        // Scene 이동
        Debug.Log("타이틀로 이동합니다.");
    }

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
