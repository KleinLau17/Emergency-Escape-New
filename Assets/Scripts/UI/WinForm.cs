using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinForm : MonoBehaviour
{
    //TODO
    [SerializeField] private Button menuButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        menuButton.onClick.AddListener(OnMenuButtonClick);
        restartButton.onClick.AddListener(OnRestartButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
    }

    private void OnEnable()
    {
        GameEvents.Win += Win;
    }

    private void OnMenuButtonClick()
    {
        SceneManager.LoadScene("Menu");
    }

    private void OnRestartButtonClick()
    {
        SceneManager.LoadScene("Main");
    }

    private void OnQuitButtonClick()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void Win()
    {

    }

    private void OnDisable()
    {
        GameEvents.Win -= Win;
    }
}
