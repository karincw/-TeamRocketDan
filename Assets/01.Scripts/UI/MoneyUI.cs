using Karin;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace JSY
{
    public class MoneyUI : MonoBehaviour
    {
        [SerializeField] private MochiDataSO data;
        [SerializeField] private TextMeshProUGUI moneyText;
        private Button buyButton;
        private int money = 0;

        private void Awake()
        {
            buyButton = transform.Find("BuyBtn").GetComponent<Button>();

            buyButton.onClick.AddListener(HandleBuyButton);
            ModifyMoney(0);
        }

        private void Update()
        {
#if UNITY_EDITOR
            if(Keyboard.current.cKey.wasPressedThisFrame)
            {
                ModifyMoney(400);
            }
#endif
        }

        private void ModifyMoney(int value)
        {
            money += value;
            moneyText.text = money + " G";
        }

        private void HandleBuyButton()
        {
            if (money < 40) return;
            ModifyMoney(-40);
            Mochi mochi = MochiManager.Instance.InstantiateMochi(data);
            mochi.transform.position = Vector2.zero;
        }
    }
}
