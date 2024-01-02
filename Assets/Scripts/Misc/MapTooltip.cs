using UnityEngine;
using TMPro;

public class MapTooltip : MonoBehaviour
{
    public TMP_Text tooltip; 

    private void Start()
    {
        ShowTooltip();
    }

    private void ShowTooltip()
    {
        tooltip.gameObject.SetActive(true);
    }
}
