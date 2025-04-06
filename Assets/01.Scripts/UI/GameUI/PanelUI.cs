using UnityEngine;

public abstract class PanelUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup panelGroup;

    public CanvasGroup PanelGroup => panelGroup;
    public Transform PanelTrm => PanelGroup.transform;
    public RectTransform PanelRectTrm => PanelTrm as RectTransform;

    public abstract void OpenPanel();
    public abstract void ClosePanel();
}
