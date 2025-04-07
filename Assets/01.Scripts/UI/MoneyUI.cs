using Karin;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace JSY
{
    public class MoneyUI : MonoBehaviour
    {
        [SerializeField] private Transform bottomUITrm;
        [SerializeField] private MochiDataSO data;
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private TextMeshProUGUI costText;
        [SerializeField] private int money = 162;
        private Button buyButton;
        private int cost = 40;

        [SerializeField] private bool _debugMode = false;

        private void Awake()
        {
            buyButton = bottomUITrm.Find("BuyBtn").GetComponent<Button>();
            buyButton.onClick.AddListener(HandleBuyButton);
        }

        private void Update()
        {
            if (SceneManager.GetActiveScene().name == "JSY") return;

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                HandleBuyButton();
            }

        }

        private void HandleChangeTurnEvent()
        {
            cost += 2;
            costText.text = cost + "G";
        }

        public void ModifyMoney(int value)
        {
            money += value;
            moneyText.text = money + " G";
        }

        private void HandleBuyButton()
        {
            if (_debugMode)
            {
                HandleChangeTurnEvent();
                Mochi mochis = MochiManager.Instance.InstantiateMochi(data);
                ModifyMoney(-cost);
                return;
            }
            if (money < cost) return;
            ModifyMoney(-cost);
            HandleChangeTurnEvent();
            Mochi mochi = MochiManager.Instance.InstantiateMochi(data);
        }
    }
}
