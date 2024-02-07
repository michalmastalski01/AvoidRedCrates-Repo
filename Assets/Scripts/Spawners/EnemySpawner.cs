using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }
    [SerializeField] private Transform enemyPrefab;
    [SerializeField] private int amountEnemiesToPool;
    [SerializeField] private float spawningPositionX;
    [SerializeField] private float spawningOffsetZ;
    [SerializeField] private float secondsToStartIncreasingSpawningSpeed;
    [SerializeField] private float timeToDecrease;

    [SerializeField] private float spawningRate = 2;

    private List<Transform> pooledEnemies; 

    private float startingSpawnRate;
    private float gameplayTimer = 0;

    private void Awake()
    {
        Instance = this;
        startingSpawnRate = spawningRate;
    }
    private void Start()
    {
        pooledEnemies = new List<Transform>();
        for(int i = 0; i < amountEnemiesToPool; i++)
        {
            Transform enemyToPool = Instantiate(enemyPrefab);
            enemyToPool.gameObject.SetActive(false);
            pooledEnemies.Add(enemyToPool);
        }
    }
    private void Update()
    {
        gameplayTimer += Time.deltaTime;

        spawningRate -= Time.deltaTime;
        if (spawningRate <= 0)
        {
            SpawnEnemy();
            spawningRate = startingSpawnRate; 
        }

        if(gameplayTimer > secondsToStartIncreasingSpawningSpeed && startingSpawnRate > 0.1f)
        {
            IncreaseSpawnSpeed();
            gameplayTimer = 0;
        }
    }

    private void IncreaseSpawnSpeed()
    {
        startingSpawnRate -= timeToDecrease;
    }

    private void SpawnEnemy()
    {
        float randomX = Random.Range(-spawningPositionX, spawningPositionX);

        Vector3 spawningPosition = new Vector3(randomX, 0, spawningOffsetZ);

        Transform pooledEnemy = GetPooledEnemy();
        pooledEnemy.position = spawningPosition;
        pooledEnemy.gameObject.SetActive(true);

    }

    private Transform GetPooledEnemy()
    {
        for(int i = 0; i < amountEnemiesToPool; i++)
        {
            if (!pooledEnemies[i].gameObject.activeInHierarchy)
            {
                return pooledEnemies[i];
            }
        }
        return null;
    }
}
