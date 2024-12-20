using Karin;
using System;
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
        [SerializeField] private TextMeshProUGUI costText;
        private Button buyButton;
        private int money = 0;
        private int cost = 40;

        protected override void Awake()
        {
            buyButton = transform.Find("BuyBtn").GetComponent<Button>();

            buyButton.onClick.AddListener(HandleBuyButton);
            ModifyMoney(162);
        }
        
        private void Update()
        {
            if (SceneManager.GetActiveScene().name == "JSY") return;

            if(Keyboard.current.spaceKey.wasPressedThisFrame)
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
            if (money < cost) return;
            ModifyMoney(-cost);
            HandleChangeTurnEvent();
            Mochi mochi = MochiManager.Instance.InstantiateMochi(data);
        }
    }
}
