using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }


    [SerializeField] private AudioClip buttonClickAudioClip;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject defaultPlayerSkin;
    [SerializeField] private List<GameObject> enviromentObjects;

    [SerializeField] private GameObject[] spawners;

    private bool isPaused;
    private bool isPlaying;
    private bool canRevive;


    public event Action<bool> OnPauseToggle;
    public event Action OnStart;
    public event Action OnGameOver;

    private Wallet wallet;

    private void Awake()
    {
        instance = this;
        isPlaying = false;
        canRevive = true;
        wallet = GetComponent<Wallet>();
        SetEnviromentActive(false);
    }

    private void Start()
    {
        foreach(GameObject spawner in spawners)
        {
            spawner.SetActive(false);
        }
        Application.targetFrameRate = 240;
    }
    public Wallet GetWallet()
    {
        if(wallet != null)
        {
            return wallet;
        }
        else
        {
            return new Wallet();
        }
    }

    private void ToggleSpawners(bool isPlaying)
    {
        foreach (GameObject spawner in spawners)
        {
            spawner.SetActive(isPlaying);
        }
    }

    //Delete this method when build. Its only for testing!
    public void AddMoney(int amount)
    {
        GetWallet().AddCoins(amount);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            wallet.AddCoins(50);
        }
    }
    public void TogglePause()
    {
        ToggleSpawners(isPlaying);
        if (isPlaying)
        {
            isPaused = !isPaused;
            OnPauseToggle?.Invoke(isPaused);
        }
    }
    public bool IsGamePaused()
    {
        return isPaused;    
    }

    public void RestartLevel()
    {
        DataPersistenceManager.Instance.SaveGame();
        SoundManager.Instance.PlaySound(buttonClickAudioClip);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        DataPersistenceManager.Instance.SaveGame();
        Application.Quit();
    }
    public void StartGame()
    {
        isPlaying = true;
        SetSkin();
        OnStart?.Invoke();
        ToggleSpawners(isPlaying);
        SetEnviromentActive(true);
        SoundManager.Instance.PlaySound(buttonClickAudioClip);
        UIManager.Instance.StartGame();
        Time.timeScale = 1f;
    }

    public void StartGameWhenPlayerRevive()
    {
        GetWallet().SubtractCoins(25);

        isPlaying = true;
        OnStart?.Invoke();
        UIManager.Instance.StartGame();
        ToggleSpawners(isPlaying);
        Time.timeScale = 1f;
    }
    public void GameOver()
    {
        PowerUpManager.Instance.EndActivePowerUp();
        isPlaying = false;
        OnGameOver?.Invoke();
        Time.timeScale = 0;

        if (canRevive && GetWallet().coins >= 25)
        {
            UIManager.Instance.SetReviveScreen(true);
            canRevive = false;
            return;
        }

        UIManager.Instance.SetReviveScreen(false);
        wallet.AddCoins(PlayerController.Instance.GetScorePoints());
        wallet.TrySetHighScore(PlayerController.Instance.GetScorePoints());
    }

    private void SetSkin()
    {
        SkinSO currentSkinSO = SkinShop.Instance.GetCurrentSkinSO();

        if (wallet.boughtSkinsList.Contains(currentSkinSO))
        {
            Instantiate(currentSkinSO.skinPrefab, player.transform);
            wallet.SetCurrentSkinId(currentSkinSO.id);
        }
        else
        {
            Instantiate(defaultPlayerSkin, player.transform);
        }
        player.GetComponent<PlayerController>().animator = player.GetComponentInChildren<Animator>();
    }

    private void SetEnviromentActive(bool isActive)
    {
        foreach (GameObject enviromentObject in enviromentObjects)
        {
            enviromentObject.SetActive(isActive);
        }
    }
}
