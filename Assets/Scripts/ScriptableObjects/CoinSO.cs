using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CoinSO : ScriptableObject
{
    public CoinType coinType;
    public Transform prefab;
    [Range(1, 100)] public float spawnRate;
}
