using UnityEngine;
using UnityEngine.UI;

public enum Speed : int
{
    normalSpeed = 1,
    twoSpeed = 2,
    fourSpeed = 4,
}

namespace JSY
{
    public class SpeedUI : MonoBehaviour
    {
        [SerializeField] private Sprite[] speedIcon;
        private Speed speed = Speed.normalSpeed;

        private Image iconImage;

        private void Awake()
        {
            iconImage = transform.Find("Icon").GetComponent<Image>();
        }

        public void ChangeSpeed()
        {
            speed = (Speed)((int)speed * 2);
            if ((int)speed == 8) speed = Speed.normalSpeed;
            iconImage.sprite = speedIcon[(int)speed];
            Time.timeScale =  (int)speed;
        }
    }
}
