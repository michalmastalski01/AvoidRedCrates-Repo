using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] private float movementSpeed;
    [SerializeField] private float maxHorizontalPosition;
    [SerializeField] private float maxVerticalPosition;
    [SerializeField] public Animator animator;
    [SerializeField] private AudioClip deathAudioClip;
    [SerializeField] private GameObject footstepParticle;
    [SerializeField] private Transform gamePlane;

    public Joystick joystick;

    private float baseMovementSpeed;
    [SerializeField] private bool isPowerUpActive;
    private float powerUpTimer = 5;
    private bool isMoving = false;

    private int scorePoints = 0;

    private Vector3 position;
    private PowerUp.PowerUpType powerUpType;
    private PowerUpManager powerUpManager;
    private Vector3 moveDirection;
    private Rigidbody rb;
    private float footstepTimer = 0.35f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        baseMovementSpeed = movementSpeed;
        isPowerUpActive = false;
        Instance = this;
        Time.timeScale = 1;
        position = transform.position;
        powerUpManager = GetComponent<PowerUpManager>();
    }

    private void Start()
    {
        GameManager.instance.OnGameOver += GameManager_OnGameOver;
    }

    private void GameManager_OnGameOver()
    {
        SoundManager.Instance.PlaySound(deathAudioClip);
        Destroy(gameObject);
    }

    private void Update()
    {
        HandleMovement();
        HandleAnimations();
    }
    private void HandleAnimations()
    {
        if (moveDirection == Vector3.zero)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
        }
        else
        {
            SoundManager.Instance.PlayFootsteps();
            SpawnFootstepParticles();
            if(movementSpeed < baseMovementSpeed)
            {
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
            }
            else
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", true);
            }
            
        }
    }
    private void SpawnFootstepParticles()
    {
        footstepTimer -= Time.deltaTime;
        if(footstepTimer < 0)
        {
            GameObject particle = Instantiate(footstepParticle, transform.position, Quaternion.identity);
            Destroy(particle, 0.5f);
            footstepTimer = 0.35f;
        }
    }
    private void HandleMovement()
    {
        //float verticalInput = Input.GetAxisRaw("Vertical");
        //float horizontalInput = Input.GetAxisRaw("Horizontal");

        float verticalInput = joystick.Vertical;
        float horizontalInput = joystick.Horizontal;
        
        Vector3 moveVector = new Vector3(horizontalInput, 0, verticalInput).normalized * movementSpeed;
        Vector3 moveVectorNormalized = new Vector3(moveVector.x * Time.deltaTime, 0, moveVector.z * Time.deltaTime);

        transform.position += moveVectorNormalized;
        moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        float smoothSpeed = 10f;
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * smoothSpeed);
        transform.position = GetClampPlayerPosition();
    }

    public void TriggerRoll()
    {
        animator.SetTrigger("triggerRoll");
    }

    private Vector3 GetClampPlayerPosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);   
        Debug.Log(mousePosition);

        return new Vector3(Mathf.Clamp(transform.position.x, -maxHorizontalPosition, maxHorizontalPosition), 0, Mathf.Clamp(transform.position.z, -maxVerticalPosition, maxVerticalPosition));
    }

    public int GetScorePoints()
    {
        return scorePoints; 
    }

    public void AddPoint()
    {
        scorePoints++;
    }

    public void SetMovementSpeed(float movementSpeed)
    {
        this.movementSpeed = movementSpeed;
    }
    public float GetBaseMovementSpeed()
    {
        return baseMovementSpeed;
    }
    public Vector3 GetCurrentPlayerPosition()
    {
        return transform.position;
    }
}
