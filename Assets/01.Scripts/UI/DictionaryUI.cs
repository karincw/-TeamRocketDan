using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct dicPanel
{
    public GameObject panel;
    public int index;
    public Button button;
}

namespace JSY
{
    public class DictionaryUI : PopupUI
    {
        [SerializeField] private List<dicPanel> dicPanels = new List<dicPanel>();

        private int index = 0;

        private void Awake()
        {
            foreach(var panel in dicPanels)
            {
                panel.button.onClick.AddListener(() => HandleDicButton(panel.index));
            }
            ExitButton.onClick.AddListener(ClosePanel);
        }

        private void HandleDicButton(int value)
        {
            dicPanels[index].panel.SetActive(false);
            index = value;
            dicPanels[index].panel.SetActive(true);
        }
    }
}
