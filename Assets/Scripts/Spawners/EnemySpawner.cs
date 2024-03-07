using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Classic,
    Missile,
    Quick
}
[System.Serializable]
public class EnemyClass
{
    public EnemyType type;
    public Transform prefab;
    [Range(1, 100)] public float spawnRate;
}


public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }
    [SerializeField] private int amountEnemiesToPool;
    [SerializeField] private float spawningPositionX;
    [SerializeField] private float spawningPositionY;
    [SerializeField] private float spawningOffsetZ;
    [SerializeField] private float secondsToStartIncreasingSpawningSpeed;
    [SerializeField] private float timeToDecrease;

    [SerializeField] private List<EnemyClass> enemyClassList;

    private List<float> cumulateProbabilityList = new List<float>();


    [SerializeField] private float spawningRate = 2;

    private float startingSpawnRate;
    private float gameplayTimer = 0;

    private void Awake()
    {
        Instance = this;
        startingSpawnRate = spawningRate;
    }

    private void Update()
    {
        gameplayTimer += Time.deltaTime;

        spawningRate -= Time.deltaTime;
        if (spawningRate <= 0)
        {
            PickRandomEnemyAndSpawn();
            spawningRate = startingSpawnRate; 
        }

        if(gameplayTimer > secondsToStartIncreasingSpawningSpeed && startingSpawnRate > 0.2f)
        {
            IncreaseSpawnSpeed();
            gameplayTimer = 0;
        }
    }

    private void GetCumulateProbability()
    {
        float cumulateProbability = 0;
        cumulateProbabilityList = new List<float>();

        foreach (EnemyClass enemy in enemyClassList)
        {
            cumulateProbability += enemy.spawnRate;
            cumulateProbabilityList.Add(cumulateProbability);
        }

        if (cumulateProbability > 100)
        {
            Debug.LogError("Cumulate probability is above 100%!");
        }

    }

    private void IncreaseSpawnSpeed()
    {
        startingSpawnRate -= timeToDecrease;
    }

    private void PickRandomEnemyAndSpawn()
    {
        float randomX = Random.Range(-spawningPositionX, spawningPositionX);
        float randomY = Random.Range(-spawningPositionY, spawningPositionY);

        GetCumulateProbability();
        EnemyClass enemyClass = GetRandomEnemyClass();  

        if(enemyClass.type == EnemyType.Classic)
        {
            SpawnClassic(enemyClass);
            return;
        }
        if(enemyClass.type == EnemyType.Missile)
        {
            SpawnMissile(enemyClass);
            return;
        }
        if (enemyClass.type == EnemyType.Quick)
        {
            SpawnClassic(enemyClass);
            return;
        }
    }

    private void SpawnClassic(EnemyClass enemyClass)
    {
        float randomX = Random.Range(-spawningPositionX, spawningPositionX);
        Vector3 spawningPosition = new Vector3(randomX, 0, spawningOffsetZ);

        Transform spawnedEnemy = Instantiate(enemyClass.prefab);
        spawnedEnemy.position = spawningPosition;
    }
    private void SpawnMissile(EnemyClass enemyClass)
    {
        float randomX = Random.Range(-spawningPositionX, spawningPositionX);
        float randomY = Random.Range(-spawningPositionY, spawningPositionY);
        Vector3 spawningPosition = new Vector3(randomX, 0, randomY);

        Transform spawnedEnemy = Instantiate(enemyClass.prefab);
        spawnedEnemy.position = spawningPosition;
    }

    private EnemyClass GetRandomEnemyClass()
    {
        int randomNumber = Random.Range(1, 101);

        for (int i = 0; i < enemyClassList.Count; i++)
        {
            if (randomNumber < cumulateProbabilityList[i])
            {
                return enemyClassList[i];
            }
        }
        return null;
    }
}
