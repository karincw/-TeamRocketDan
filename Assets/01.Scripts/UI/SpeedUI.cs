using UnityEngine;
using UnityEngine.UI;

namespace JSY
{
    public class SpeedUI : MonoBehaviour
    {
        [SerializeField] private Button speedButton;
        [SerializeField] private Sprite[] speedIcon;

        private Image iconImage;

        private void Awake()
        {
            speedButton.onClick.AddListener(ChangeSpeed);
            iconImage = speedButton.transform.Find("Icon").GetComponent<Image>();
        }

        public void ChangeSpeed()
        {
            Speed speed = GameUISystem.ChangeSpeed();
            iconImage.sprite = speedIcon[(int)speed];
            Time.timeScale =  (int)speed;
        }
    }
}
