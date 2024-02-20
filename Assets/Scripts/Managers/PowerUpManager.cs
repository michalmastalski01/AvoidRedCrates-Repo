using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance { get; private set; }
    private float powerUpTimer = 5;
    private bool isPowerUpActive = false;
    private PowerUp.PowerUpType powerUpType;

    private PowerUpSO activePowerUpSO;

    private PlayerController playerController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There is more than one PowerUpManager instance!");
        }
    }
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (isPowerUpActive)
        {
            ActivatePowerUp(powerUpType);
        }
    }
    public void AddPowerUp(PowerUp.PowerUpType powerUpType, PowerUpSO powerUpSO)
    {
        EndActivePowerUp();
        isPowerUpActive = true;
        powerUpTimer = powerUpSO.powerUpDuration;

        activePowerUpSO = powerUpSO;
        this.powerUpType = powerUpType;
    }
    public void ActivatePowerUp(PowerUp.PowerUpType powerUpType)
    {
        powerUpTimer -= Time.deltaTime;

        switch (powerUpType)
        {
            case PowerUp.PowerUpType.TimeFreeze:
                FreezeTime();
                break;
            case PowerUp.PowerUpType.TimeSpeedUp:
                SpeedUpTime();
                break;
            case PowerUp.PowerUpType.DestroyEnemies:
                DestroyEnemies();
                break;
            case PowerUp.PowerUpType.AttractCoins:
                AttractCoins();
                break;
            default: break;
        }

        if (powerUpTimer < 0)
        {
            EndActivePowerUp();
        }
    }
    private void FreezeTime()
    {
        Time.timeScale = 0.5f;
    }
    private void SpeedUpTime()
    {
        Time.timeScale = 2f;
    }
    private void DestroyEnemies()
    {
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        foreach(Enemy enemy in enemies)
        {
            enemy.Disable();
        }
    }
    private void AttractCoins()
    {
        Coin[] coins = GameObject.FindObjectsOfType<Coin>();
        foreach (Coin coin in coins)
        {
            coin.MoveCoinToPlayer(PlayerController.Instance.GetCurrentPlayerPosition());
        }
    }

    public void EndActivePowerUp()
    {
        Time.timeScale = 1f;
        isPowerUpActive = false;
    }

    public float GetPowerUpTimer()
    {
        return powerUpTimer;
    }
    public bool IsPowerUpActive()
    {
        return isPowerUpActive;
    }
    public PowerUpSO GetActivePowerUpSO()
    {
        return activePowerUpSO;
    }
}
