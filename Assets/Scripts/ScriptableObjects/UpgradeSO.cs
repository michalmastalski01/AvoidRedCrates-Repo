using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    Roll,
    MovementBoostI,
    MovementBoostII,
    MovementBoostIII
    //You can add more upgradeTypes.
}


[CreateAssetMenu()]
public class UpgradeSO : ScriptableObject
{
    public int id;
    public string upgradeName;
    public string upgradeDescription;
    public UpgradeType upgradeType;
    public Sprite upgradeIcon;
    public int upgradeCost;
}
