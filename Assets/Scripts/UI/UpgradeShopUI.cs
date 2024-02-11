using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShopUI : ShopUI
{
    private void Awake()
    {
        //It is obligatory to prevent spawning more items, when player refresh scene.
        DestroyAllObjects();

        List<UpgradeSO> upgradeSOList = UpgradeShop.Instance.GetUpgradeSOList();
        foreach (UpgradeSO upgradeSO in upgradeSOList)
        {
            GameObject itemGameObject = Instantiate(itemPrefab, itemContainer);
            UpgradeItemUI itemUI = itemGameObject.GetComponent<UpgradeItemUI>();
            itemUI.SetUpgradeSO(upgradeSO);
        }
    }

}
