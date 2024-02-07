using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } 

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject skinPreviewModel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI scoreTextOnGameOver;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private AudioClip buttonClickAudioClip;

    private bool isPaused = false;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        startScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        gameScreen.SetActive(false);
        skinPreviewModel.SetActive(true);
        GameManager.instance.OnGameOver += GameManager_OnGameOver;
        GameManager.instance.OnPauseToggle += GameManager_OnPauseToggle;

    }

    private void GameManager_OnPauseToggle(bool obj)
    {
        isPaused = obj;
        PauseGame();
    }

    private void GameManager_OnGameOver()
    {
        scoreTextOnGameOver.text = "Score: " + PlayerController.Instance.GetScorePoints();
        gameOverScreen.SetActive(true);
    }

    private void Update()
    {
        scoreText.text = "Score: " + PlayerController.Instance.GetScorePoints();
        highScoreText.text = "High Score: " + GameManager.instance.GetWallet().highScore.ToString();
    }

    public void StartGame()
    {
        startScreen.SetActive(false);
        gameScreen.SetActive(true);
        skinPreviewModel.SetActive(false);
    }

    private void PauseGame()
    {
        if (isPaused) 
        {
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
        }
    }
}
