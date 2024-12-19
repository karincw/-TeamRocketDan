using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace JSY
{
    public class OptionUI : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private RectTransform popupUI;
        [SerializeField] private RectTransform mochiInfo;
        [SerializeField] private RectTransform enemyInfo;
        [SerializeField] private CanvasGroup popupCanvasGroup;
        [SerializeField] private Button popupCancelButton, mochiCancelButton, enemyCancelButton;

        private Button mochiInfoButton, enemyInfoButton, exitButton;

        private bool isOpen = false;
        private bool isAnimating = false;
        private bool isDicOpen = false;
        private bool isMochi = false;

        float durationTime = 0.2f;

        private void Awake()
        {
            Transform panel = transform.GetChild(1).GetChild(1);
            mochiInfoButton = panel.Find("MochiInfoBtn").GetComponent<Button>();
            enemyInfoButton = panel.Find("EnemyInfoBtn").GetComponent<Button>();
            exitButton = panel.Find("ExitBtn").GetComponent<Button>();

            mochiInfoButton.onClick.AddListener(SettingMochiInfoPanel);
            enemyInfoButton.onClick.AddListener(SettingEnemyInfoPanel);
            exitButton.onClick.AddListener(HandleExitButton);

            popupCancelButton.onClick.AddListener(SettingOptionPanel);
            mochiCancelButton.onClick.AddListener(SettingMochiInfoPanel);
            enemyCancelButton.onClick.AddListener(SettingEnemyInfoPanel);
        }
        private void HandleExitButton()
        {
            SceneManager.LoadScene("TitleScene");
        }

        private void Update()
        {
            if (SceneManager.GetActiveScene().name == "JSY") return;

            if (Keyboard.current.escapeKey.wasReleasedThisFrame)
            {
                if (!isDicOpen)
                    SettingOptionPanel();
                if (isDicOpen)
                {
                    if(isMochi)
                        SettingInfoPanel(mochiInfo);
                    else
                        SettingInfoPanel(enemyInfo);
                }
            }
        }

        public void SettingOptionPanel()
        {
            isOpen = !isOpen;
            popupCanvasGroup.blocksRaycasts = isOpen;
            SettingPanel(popupUI, isOpen);
        }

        public void SettingMochiInfoPanel()
        {
            isMochi = true;
            SettingInfoPanel(mochiInfo);
        }

        public void SettingEnemyInfoPanel()
        {
            isMochi = false;
            SettingInfoPanel(enemyInfo);
        }

        public void SettingInfoPanel(RectTransform panel)
        {
            isDicOpen = !isDicOpen;
            SettingPanel(panel, isDicOpen);
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
                    if (!this.isOpen && !isDicOpen)
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
            if (isEnd)
                background.gameObject.SetActive(isTrue);
            panel.gameObject.SetActive(isTrue);
        }
    }

}