using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : MonoBehaviour
{
    [SerializeField] GameObject powerUpUI;
    [SerializeField] Image powerUpImage;
    [SerializeField] Slider powerUpSlider;
    private float timer;
    private void Start()
    {
        powerUpUI.SetActive(false);
    }
    private void Update()
    {
        if (PowerUpManager.Instance.IsPowerUpActive())
        {
            powerUpUI.SetActive(true);
            powerUpImage.sprite = PowerUpManager.Instance.GetActivePowerUpSO().powerUpSprite;
            UpdatePowerUpUI();
        } 
        else
        {
            powerUpUI.SetActive(false);
        }
    }
    private void UpdatePowerUpUI()
    {
        float powerUpDuration = PowerUpManager.Instance.GetActivePowerUpSO().powerUpDuration;
        timer = PowerUpManager.Instance.GetPowerUpTimer() / powerUpDuration;

        powerUpSlider.value = timer;
    }
}
