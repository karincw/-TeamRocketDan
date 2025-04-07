using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PopupUI : PanelUI
{
    [SerializeField] private Button exitButton;
    private float fadeDuration = 0.3f;
    private bool isPanel = false;

    public bool IsPanel => isPanel;
    public Button ExitButton => exitButton;

    public override void OpenPanel() => SetPanel(true);

    public override void ClosePanel() => SetPanel(false);

    private void SetPanel(bool isActive)
    {
        Time.timeScale = isActive ? 0 : (int)GameUISystem.GameSpeed;
        PanelGroup.DOFade(isActive ? 1f : 0f, fadeDuration).SetUpdate(true);
        PanelGroup.blocksRaycasts = isActive;
        PanelGroup.interactable = isActive;
    }
}
