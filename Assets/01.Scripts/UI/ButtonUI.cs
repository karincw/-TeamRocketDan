using System;
using UnityEngine;
using UnityEngine.UI;

namespace JSY
{
    public class ButtonUI : MonoBehaviour
    {
        [SerializeField] private Button speedButton, optionButton;

        private void Start()
        {
            if(speedButton != null)
                speedButton.onClick.AddListener(UIManager.Instance.speedUI.ChangeSpeed);
            if(optionButton != null)
                optionButton.onClick.AddListener(UIManager.Instance.optionUI.SettingOptionPanel);
        }
    }
}
