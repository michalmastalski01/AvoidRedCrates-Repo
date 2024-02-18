using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManger : MonoBehaviour
{
    List<UpgradeSO> activeUpgradeSOList = new List<UpgradeSO>();

    [Header("Roll components:")]
    [SerializeField] private GameObject rollButton;

    private void Start()
    {
        rollButton.SetActive(false);
        activeUpgradeSOList = GameManager.instance.GetWallet().boughtUpgradesList;
        EnableUpgrades();
    }

    private void EnableUpgrades()
    {
        Wallet wallet = GameManager.instance.GetWallet();
        foreach(UpgradeSO upgradeSO in activeUpgradeSOList)
        {
            switch(upgradeSO.upgradeType) 
            {
                case UpgradeType.MovementBoostI:
                    EnableMovementBoostI();
                    break;
                case UpgradeType.MovementBoostII:
                    EnableMovementBoostII();
                    break;
                case UpgradeType.MovementBoostIII:
                    EnableMovementBoostIII();
                    break;
                case UpgradeType.Roll:
                    EnableRoll();
                    break;
            }
        }
    }

    private void EnableMovementBoostI()
    {
        PlayerController.Instance.SetMovementSpeed(PlayerController.Instance.GetBaseMovementSpeed() + 1);
    }
    private void EnableMovementBoostII()
    {
        PlayerController.Instance.SetMovementSpeed(PlayerController.Instance.GetBaseMovementSpeed() + 2);
    }
    private void EnableMovementBoostIII()
    {
        PlayerController.Instance.SetMovementSpeed(PlayerController.Instance.GetBaseMovementSpeed() + 3);
    }

    private void EnableRoll()
    {
        rollButton.SetActive(true);
    }
}
