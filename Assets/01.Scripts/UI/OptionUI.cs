using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private RectTransform popupUI;
    [SerializeField] private RectTransform infoUI;
    [SerializeField] private CanvasGroup popupCanvasGroup;

    private Slider bgmSlider, sfxSlider;
    private Button infoButton, exitButton;

    private bool isOpen = false;
    private bool isAnimating = false;
    private bool isDicOpen = false;

    float durationTime = 0.2f;

    private void Awake()
    {
        Transform panel = transform.GetChild(1).GetChild(1);
        bgmSlider = panel.Find("BGMSlider").GetComponent<Slider>();
        sfxSlider = panel.Find("SFXSlider").GetComponent<Slider>();
        infoButton = panel.Find("InfoBtn").GetComponent<Button>();
        exitButton = panel.Find("ExitBtn").GetComponent<Button>();

        infoButton.onClick.AddListener(SettingInfoPanel);
        exitButton.onClick.AddListener(HandleExitButton);
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
            if (!isDicOpen)
                SettingOptionPanel();
            if (isDicOpen)
                SettingInfoPanel();
        }
    }

    public void SettingOptionPanel()
    {
        isOpen = !isOpen;
        popupCanvasGroup.blocksRaycasts = isOpen;
        SettingPanel(popupUI, isOpen);
    }
    
    public void SettingInfoPanel()
    {
        isDicOpen = !isDicOpen;
        SettingPanel(infoUI, isDicOpen);
    }

    private void SettingPanel(RectTransform panel, bool isOpen)
    {
        if (isAnimating) return;
        isAnimating = true;

        if (isOpen)
            ActiveUI(panel, true, true);

        float fadeValue = isOpen ? 0.6f : 0f;
        float sizeValue = isOpen ? 1f : 0f;

        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            if (isOpen || (!this.isOpen && !isDicOpen))
                background.DOFade(fadeValue, durationTime);
        });
        seq.Join(panel.DOScale(sizeValue, durationTime));
        seq.OnComplete(() =>
        {
            
            isAnimating = false;
            if (!isOpen)
            {
                if(!this.isOpen && !isDicOpen)
                {
                    ActiveUI(panel, false, true);
                }
                else
                    ActiveUI(panel, false);
            }
        });
    }

    private void ActiveUI(RectTransform panel, bool isTrue, bool isEnd = false)
    {
        if(isEnd)
            background.gameObject.SetActive(isTrue);
        panel.gameObject.SetActive(isTrue);
    }
}
