using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System;

[System.Serializable]
public enum Quality
{
    Low,
    Medium,
    High
}

public class SettingsScreen : MonoBehaviour, IDataPersistence
{
    public static SettingsScreen Instance { get; private set; }

    [SerializeField] private Animator settingsToggleSwitchAnimator;
    [SerializeField] private SettingsButtons settingsButtons;
 
    private Quality currentQualitySettings;

    private void Awake()
    {
        Instance = this;
    }

    public void SetCurrentQualityLevel(int qualityId)
    {
        if(qualityId > 2)
        {
            return;
        }
        currentQualitySettings = (Quality)qualityId;

        Debug.Log(currentQualitySettings.ToString());

        switch (currentQualitySettings)
        {
            case Quality.Low:
                QualitySettings.SetQualityLevel(0, true);
                break;
            case Quality.Medium:
                QualitySettings.SetQualityLevel(1, true);
                break;
            case Quality.High:
                QualitySettings.SetQualityLevel(2, true);
                break;
            default:
                QualitySettings.SetQualityLevel(1, true);
                break;
        }
        settingsButtons.OnQualityChange();
    }

    public void LoadData(GameData gameData)
    {
        SetCurrentQualityLevel((int)gameData.currentQuality);
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.currentQuality = currentQualitySettings;
    }
    public Quality GetCurrentQualitySettings()
    {
        return currentQualitySettings;
    }
}
