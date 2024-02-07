using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsButtons : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] qualityButtons;

    public void OnQualityChange()
    {
        foreach(TextMeshProUGUI qualityButton in qualityButtons)
        {
            if (qualityButton.text == SettingsScreen.Instance.GetCurrentQualitySettings().ToString())
            {
                qualityButton.color = Color.gray;
            }
            else
            {
                qualityButton.color = Color.white;
            }
        }
    }
}
