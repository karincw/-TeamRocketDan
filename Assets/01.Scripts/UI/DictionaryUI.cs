using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JSY
{
    public class DictionaryUI : MonoBehaviour
    {
        [SerializeField] private List<GameObject> dicPanels = new List<GameObject>();
        private Button star1Button, star2Button, star3Button, star4Button, star5Button;

        private int index = 0;

        private void Awake()
        {
            Transform buttonGroup = transform.GetChild(0).GetChild(0).Find("ButtonGroup");

            star1Button = buttonGroup.Find("Star1Btn").GetComponent<Button>();
            star2Button = buttonGroup.Find("Star2Btn").GetComponent<Button>();
            star3Button = buttonGroup.Find("Star3Btn").GetComponent<Button>();
            star4Button = buttonGroup.Find("Star4Btn").GetComponent<Button>();
            star5Button = buttonGroup.Find("Star5Btn").GetComponent<Button>();

            star1Button.onClick.AddListener(() => HandleDicButton(0));
            star2Button.onClick.AddListener(() => HandleDicButton(1));
            star3Button.onClick.AddListener(() => HandleDicButton(2));
            star4Button.onClick.AddListener(() => HandleDicButton(3));
            star5Button.onClick.AddListener(() => HandleDicButton(4));
        }

        private void HandleDicButton(int value)
        {
            dicPanels[index].SetActive(false);
            index = value;
            dicPanels[index].SetActive(true);
        }
    }
}
