using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuForm : MonoBehaviour
{
    [SerializeField] private ToggleGroup toggleGroup;

    private Toggle currentSelection => toggleGroup.GetFirstActiveToggle();
    private Toggle onToggle;

    private void Start()
    {
        var toggles = toggleGroup.GetComponentsInChildren<Toggle>();
        foreach (var toggle in toggles)
        {
            toggle.onValueChanged.AddListener(_ => OnToggleValueChanged(toggle));
        }

        currentSelection.onValueChanged?.Invoke(true);
    }

    private void OnToggleValueChanged(Toggle toggle)
    {
        if (currentSelection == onToggle)
        {
            switch (toggle.name)
            {
                case "GameStart":
                    SceneManager.LoadScene("Game");
                    break;
                case "Settings":
                    //TODO
                    break;
                case "Quit":
                    {
                        Application.Quit();
#if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
#endif
                    }
                    break;
                default:
                    throw new UnityException("...");

            }
            return;
        }

        if (toggle.isOn)
        {
            onToggle = toggle;
            onToggle.transform.Find("Label").GetComponent<TMP_Text>().color = Color.yellow;
        }
        else
        {
            onToggle.transform.Find("Label").GetComponent<TMP_Text>().color = Color.white;
        }
    }
}
