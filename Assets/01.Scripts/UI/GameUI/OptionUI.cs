using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace JSY
{
    public class OptionUI : PopupUI
    {
        [SerializeField] private Button optionButton;
        [SerializeField] private DictionaryUI mochiDict;
        [SerializeField] private DictionaryUI enemyDict;
        [SerializeField] private Button mochiCancelButton, enemyCancelButton;

        private Button mochiInfoButton, enemyInfoButton, titleButton;

        private bool isDicOpen = false;
        private bool isMochi = false;

        private void Awake()
        {
            Transform bottomPanel = PanelTrm.Find("PopupUI").Find("BottomPanel");
            mochiInfoButton = bottomPanel.Find("MochiInfoBtn").GetComponent<Button>();
            enemyInfoButton = bottomPanel.Find("EnemyInfoBtn").GetComponent<Button>();
            titleButton = bottomPanel.Find("ExitBtn").GetComponent<Button>();

            mochiInfoButton.onClick.AddListener(SettingMochiInfoPanel);
            enemyInfoButton.onClick.AddListener(SettingEnemyInfoPanel);
            titleButton.onClick.AddListener(HandleTitleButton);
            
            mochiCancelButton.onClick.AddListener(SettingMochiInfoPanel);
            enemyCancelButton.onClick.AddListener(SettingEnemyInfoPanel);
            optionButton.onClick.AddListener(OpenPanel);
            ExitButton.onClick.AddListener(ClosePanel);
        }
        private void HandleTitleButton()
        {
            SceneManager.LoadScene("TitleScene");
        }

        private void Update()
        {
            if (SceneManager.GetActiveScene().name == "JSY") return;

            if (Keyboard.current.escapeKey.wasReleasedThisFrame)
            {
                if (IsPanel)
                {
                    if (!isDicOpen)
                        ClosePanel();
                    if (isDicOpen)
                    {
                        if (isMochi)
                            mochiDict.ClosePanel();
                        else
                            enemyDict.ClosePanel();
                    }
                }
                else
                    OpenPanel();
            }
        }


        public void SettingMochiInfoPanel()
        {
            isMochi = true;
            isDicOpen = !isDicOpen;
            mochiDict.OpenPanel();
        }

        public void SettingEnemyInfoPanel()
        {
            isMochi = false;
            isDicOpen = !isDicOpen;
            enemyDict.OpenPanel();
        }
    }

}