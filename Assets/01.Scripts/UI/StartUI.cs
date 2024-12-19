using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    private TextMeshProUGUI titleText;
    private Button startButton, exitButton;

    private void Awake()
    {
        titleText = transform.Find("TitleTxt").GetComponent<TextMeshProUGUI>();
        startButton = transform.Find("StartBtn").GetComponent<Button>();
        exitButton = transform.Find("ExitBtn").GetComponent<Button>();

        startButton.onClick.AddListener(HandleStartButton);
        exitButton.onClick.AddListener(HandleExitButton);
    }

    private void Start()
    {
        titleText.rectTransform.DOScale(1f, 1f).SetEase(Ease.OutBounce);
    }

    private void HandleStartButton()
    {
        Debug.Log("������ �����մϴ�. ���� ����� ���� �����ϴ�.");
        // Scene
    }

    private void HandleExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif
    }
}
