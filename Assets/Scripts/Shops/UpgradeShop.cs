using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShop : MonoBehaviour
{
    public static UpgradeShop Instance {  get; private set; }

    public event Action<UpgradeSO> OnBuyUpgrade;
    public event Action OnClick;

    [SerializeField] private GameObject itemContainer;
    [SerializeField] private List<UpgradeSO> upgradeSOList;

    private UpgradeSO currentUpgradeSO;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void BuyUpgrade()
    {
        Wallet wallet = GameManager.instance.GetWallet();
        if (wallet.coins >= currentUpgradeSO.upgradeCost)
        {
            if (!wallet.boughtUpgradesList.Contains(GetCurrentUpgradeSO()))
            {
                wallet.SubtractCoins(currentUpgradeSO.upgradeCost);
                wallet.AddUpgrade(currentUpgradeSO);
                OnBuyUpgrade?.Invoke(GetCurrentUpgradeSO());
            }
        }
    }

    public void SetCurrentUpgradeSO(UpgradeSO upgradeSO)
    {
        currentUpgradeSO = upgradeSO;
        OnClick?.Invoke();
    }

    public UpgradeSO GetCurrentUpgradeSO()
    {
        return currentUpgradeSO;
    }
    public List<UpgradeSO> GetUpgradeSOList()
    {
        return upgradeSOList;
    }

}
