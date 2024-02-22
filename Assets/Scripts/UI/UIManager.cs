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
    [SerializeField] private GameObject reviveScreen;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject skinPreviewModel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI scoreTextOnGameOver;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private AudioClip buttonClickAudioClip;

    [Header("Game:")]
    [SerializeField] private GameObject addingCoinTextPrefab;
    [SerializeField] private RectTransform addingCoinTextPosition;

    private bool isPaused = false;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        startScreen.SetActive(true);
        pauseButton.SetActive(false);
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
        scoreText.text = PlayerController.Instance.GetScorePoints().ToString();
        highScoreText.text = "High Score: " + GameManager.instance.GetWallet().highScore.ToString();
    }

    public void StartGame()
    { 
        pauseButton.SetActive(true);
        startScreen.SetActive(false);
        gameScreen.SetActive(true);
        skinPreviewModel.SetActive(false);
        gameOverScreen.SetActive(false);
    }

    public void AddCoinInfo(int amount)
    {
        GameObject addText = Instantiate(addingCoinTextPrefab, addingCoinTextPosition);
        addText.GetComponentInChildren<TextMeshProUGUI>().text = "+" + amount;
        addText.GetComponent<Animator>().SetTrigger("trigger");
    }

    public void SetReviveScreen(bool toggle)
    {
        reviveScreen.SetActive(toggle);
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
