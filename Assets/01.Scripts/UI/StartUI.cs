using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    private TextMeshProUGUI titleText;
    private Button startButton, tutoButton, exitButton, storyButton;

    private void Awake()
    {
        titleText = transform.Find("TitleTxt").GetComponent<TextMeshProUGUI>();
        startButton = transform.Find("StartBtn").GetComponent<Button>();
        storyButton = transform.Find("StoryBtn").GetComponent<Button>();
        tutoButton = transform.Find("TutoBtn").GetComponent<Button>();
        exitButton = transform.Find("ExitBtn").GetComponent<Button>();

        startButton.onClick.AddListener(HandleStartButton);
        tutoButton.onClick.AddListener(HandleTutoButton);
        exitButton.onClick.AddListener(HandleExitButton);
        storyButton.onClick.AddListener(HandleStoryButton);

        Time.timeScale = 1f;
    }

    private void Start()
    {
        titleText.rectTransform.DOScale(1f, 1f).SetEase(Ease.OutBounce);
    }

    private void HandleTutoButton()
    {
        SceneManager.LoadScene("JSY");
    }

    private void HandleStartButton()
    {
        SceneManager.LoadScene("CutScene");
    }

    private void HandleStoryButton()
    {
        SceneManager.LoadScene("StoryCutScene");
    }

    private void HandleExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
