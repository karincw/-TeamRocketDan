using UnityEngine;
using UnityEngine.UI;

public class SpeedUI : MonoBehaviour
{
    [SerializeField] private Sprite[] speedIcon;

    private Image iconImage;

    private float doubleSpeed = 2f, normalSpeed = 1f;
    private bool isDouble = false;

    private void Awake()
    {
        iconImage = transform.Find("Icon").GetComponent<Image>();
    }

    public void ChangeSpeed()
    {
        isDouble = !isDouble;

        iconImage.sprite = isDouble ? speedIcon[1] : speedIcon[0];
        Time.timeScale = isDouble ? doubleSpeed : normalSpeed;
    }
}
