using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public OptionUI optionUI { get; private set; }
    public ButtonUI buttonUI { get; private set; }
    public SpeedUI speedUI { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        optionUI = GetComponentInChildren<OptionUI>();
        buttonUI = GetComponentInChildren<ButtonUI>();
        speedUI = buttonUI.GetComponentInChildren<SpeedUI>();
    }
}
