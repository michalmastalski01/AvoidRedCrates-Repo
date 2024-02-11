using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class UpgradeSO : ScriptableObject
{
    public int id;
    public string upgradeName;
    public Sprite upgradeIcon;
    public int upgradeCost;
}
