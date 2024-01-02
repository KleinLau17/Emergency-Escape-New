/*using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour
{
    [SerializeField] private Button menuButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;
    private void Awake()
    {
        menuButton.onClick.AddListener(OnMenuButtonClick);
        restartButton.onClick.AddListener(OnRestartButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
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
}
*/
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu"); // 加载名为 "Menu" 的场景
    }

    public void RestartScene()
    {
        SceneManager.LoadScene("Main"); // 重新加载当前场景
    }

    public void QuitGame()
    {
        Application.Quit(); // 退出游戏
    }
}
