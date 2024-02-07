using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private float maxHorizontalPosition;
    [SerializeField] private float maxVerticalPosition;
    [SerializeField] private Transform pointPrefab;

    private float timer = 5;

    private void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            SpawnPoint();
            timer = Random.Range(1, 5);
        }
    }

    private void SpawnPoint()
    {
        float randomX = Random.Range(-maxHorizontalPosition, maxHorizontalPosition);
        float randomZ = Random.Range(-maxVerticalPosition, maxVerticalPosition);
        Vector3 spawnPosition = new Vector3(randomX, 0, randomZ);

        Instantiate(pointPrefab, spawnPosition, Quaternion.identity);
    }
}
