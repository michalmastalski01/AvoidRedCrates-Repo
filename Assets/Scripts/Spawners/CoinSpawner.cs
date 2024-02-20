using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoinType
{
    basic,
    mega
}

[System.Serializable]
public class CoinClass
{
    public CoinType coinType;
    public Transform prefab;
    [Range(1, 100)] public float spawnRate;
}

public class CoinSpawner : MonoBehaviour
{
    public static CoinSpawner Instance {  get; private set; }

    [SerializeField] private float maxHorizontalPosition;
    [SerializeField] private float maxVerticalPosition;
    [SerializeField] private List<CoinClass> coinSOList;

    private List<float> cumulateProbabilityList = new List<float>();

    private float timer = 5;


    private void Awake()
    {
        Instance = this;
    }

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

        GetCumulateProbability();
        Transform coinPrefab = GetRandomCoin();


        Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
    }
    private void GetCumulateProbability()
    {
        float cumulateProbability = 0;
        cumulateProbabilityList = new List<float>();

        foreach (CoinClass coinSO in coinSOList)
        {
            cumulateProbability += coinSO.spawnRate;
            cumulateProbabilityList.Add(cumulateProbability);
        }

        if(cumulateProbability > 100)
        {
            Debug.LogError("Cumulate probability is above 100%!");
        }
        
    }
    private Transform GetRandomCoin()
    {
        int randomNumber = Random.Range(1, 101);

        for(int i = 0; i < coinSOList.Count; i++)
        {
            if (randomNumber < cumulateProbabilityList[i])
            {
                return coinSOList[i].prefab;
            }
        }
        return null;
    }

    public void ChangeProbability(CoinType coinType, float newProbability)
    {
        foreach(CoinClass coinSO in coinSOList)
        {
            if(coinSO.coinType == coinType)
            {
                coinSO.spawnRate = newProbability;
            }
        }
    }
}
