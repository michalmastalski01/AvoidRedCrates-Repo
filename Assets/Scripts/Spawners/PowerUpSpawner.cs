using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private PowerUp[] powerUpPrefabArray;
    [SerializeField] private float maxHorizontalPosition;
    [SerializeField] private float maxVerticalPosition;
    [SerializeField] private float maxTimeToSpawn;

    float Timer;


    private void Start()
    {
        Timer = Random.Range(3, maxTimeToSpawn);
    }
    private void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            SpawnPowerUp();
            Timer = Random.Range(3, maxTimeToSpawn);
        }
    }

    private void SpawnPowerUp()
    {
        PowerUp randomPowerUp = powerUpPrefabArray[Random.Range(0, powerUpPrefabArray.Length)];

        float randomX = Random.Range(-maxHorizontalPosition, maxHorizontalPosition);
        float randomZ = Random.Range(-maxVerticalPosition, maxVerticalPosition);
        Vector3 spawnPosition = new Vector3(randomX, 0, randomZ);

        Instantiate(randomPowerUp.gameObject, spawnPosition, Quaternion.identity); 
    }

}
