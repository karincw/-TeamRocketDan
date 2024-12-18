using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour
{
    private Button speedButton, optionButton;

    private void Awake()
    {
        speedButton = transform.Find("SpeedBtn").GetComponent<Button>();
        optionButton = transform.Find("OptionBtn").GetComponent<Button>();
    }
    private void Start()
    {
        speedButton.onClick.AddListener(UIManager.Instance.speedUI.ChangeSpeed);
        optionButton.onClick.AddListener(UIManager.Instance.optionUI.SettingPanel);
    }
}
