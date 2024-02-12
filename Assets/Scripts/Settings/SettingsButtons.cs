using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsButtons : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] qualityButtons;
    [SerializeField] public Color activeColor = new Color();
    [SerializeField] public Color disableColor = new Color();

    public void OnQualityChange()
    {
        Debug.Log("Active color: " + activeColor);
        Debug.Log("Disbale color:  " + disableColor);
        foreach(TextMeshProUGUI qualityButton in qualityButtons)
        {
            if (qualityButton.text == SettingsScreen.Instance.GetCurrentQualitySettings().ToString())
            {
                qualityButton.color = activeColor;
            }
            else
            {
                qualityButton.color = disableColor;
            }
        }
    }
}
