using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SkinSO : ScriptableObject
{
    public int id;
    public string skinName;
    public GameObject skinPrefab;
    public int skinCost;
    public Rarity.RarityLevel rarityLevel;

}
