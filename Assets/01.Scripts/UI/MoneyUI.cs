using Karin;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace JSY
{
    public class MoneyUI : MonoSingleton<MoneyUI>
    {
        [SerializeField] private MochiDataSO data;
        [SerializeField] private TextMeshProUGUI moneyText;
        private Button buyButton;
        private int money = 0;

        protected override void Awake()
        {
            buyButton = transform.Find("BuyBtn").GetComponent<Button>();

            buyButton.onClick.AddListener(HandleBuyButton);
            ModifyMoney(150);
        }

        private void Update()
        {
            if (SceneManager.GetActiveScene().name == "JSY") return;

            if(Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                HandleBuyButton();
            }
        }

        public void ModifyMoney(int value)
        {
            money += value;
            moneyText.text = money + " G";
        }

        private void HandleBuyButton()
        {
            if (money < 40) return;
            ModifyMoney(-40);
            Mochi mochi = MochiManager.Instance.InstantiateMochi(data);
        }
    }
}
