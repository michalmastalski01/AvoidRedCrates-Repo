using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpSO powerUpSO;
    [SerializeField] private MeshRenderer powerUpMeshRenderer;
    [SerializeField] private GameObject breakingParticle;
    [SerializeField] private AudioClip pickUpSoundClip;
    [SerializeField] private Slider slider;

    private float timer;
    private float timerStartValue = 15;
    public enum PowerUpType
    {
        TimeFreeze,
        TimeSpeedUp,
        DestroyEnemies,
        AttractCoins
    }

    private void Start()
    {
        powerUpMeshRenderer.material.color = powerUpSO.powerUpColor;
        timer = timerStartValue;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        slider.value = timer / timerStartValue;

        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CreateBreakingParticle();
            PowerUpManager.Instance.AddPowerUp(powerUpSO.powerUpType, powerUpSO);
            SoundManager.Instance.PlaySound(pickUpSoundClip);

            Destroy(this.gameObject);
        }
    }
    private void CreateBreakingParticle()
    {
        GameObject breakingParticleObject = Instantiate(breakingParticle, transform.position, Quaternion.identity);
        ParticleSystem particleSystem = breakingParticleObject.GetComponent<ParticleSystem>();
        var main = particleSystem.main;
        Debug.Log(powerUpSO.powerUpColor);
        main.startColor = powerUpSO.powerUpColor;
        Destroy(breakingParticleObject, 0.5f);
    }

}
