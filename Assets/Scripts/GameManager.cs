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

    [SerializeField] private GameObject[] spawners;

    private bool isPaused;
    private bool isPlaying;

    public event Action<bool> OnPauseToggle;
    public event Action OnStart;
    public event Action OnGameOver;

    private Wallet wallet;

    private void Awake()
    {
        instance = this;
        isPlaying = false;
        wallet = GetComponent<Wallet>();
    }

    private void Start()
    {
        foreach(GameObject spawner in spawners)
        {
            spawner.SetActive(false);
        }
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
    private void TogglePause()
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
        SoundManager.Instance.PlaySound(buttonClickAudioClip);
        UIManager.Instance.StartGame();
        Time.timeScale = 1f;
    }
    public void GameOver()
    {
        isPlaying = false;
        OnGameOver?.Invoke();
        wallet.AddCoins(PlayerController.Instance.GetScorePoints());
        wallet.TrySetHighScore(PlayerController.Instance.GetScorePoints());
        Time.timeScale = 0;
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
}
