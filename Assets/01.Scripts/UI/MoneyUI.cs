using Karin;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JSY
{
    public class MoneyUI : MonoBehaviour
    {
        [SerializeField] private MochiDataSO data;
        private Button buyButton;
        private TextMeshProUGUI moneyText;
        private int money;

        private void Awake()
        {
            buyButton = transform.Find("BuyBtn").GetComponent<Button>();

            buyButton.onClick.AddListener(HandleBuyButton);
        }

        private void HandleBuyButton()
        {
            Karin.Mochi mochi = MochiManager.Instance.InstantiateMochi(data);
            mochi.transform.position = Vector2.zero;
        }
    }
}
