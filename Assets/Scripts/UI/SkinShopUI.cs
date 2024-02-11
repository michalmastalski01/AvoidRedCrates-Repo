using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinShopUI : ShopUI
{
    [SerializeField] private TextMeshProUGUI coinAmountText;
    [SerializeField] private Transform previewSkinPoint;
    private void Awake()
    {
        //It is obligatory to prevent spawning more items, when player refresh scene.
        DestroyAllObjects();

        List<SkinSO> skinSOList = SkinShop.Instance.GetSkinSOList();
        foreach(SkinSO skinSO in skinSOList)
        {
            GameObject itemGameObject = Instantiate(itemPrefab, itemContainer);
            ItemUI itemUI = itemGameObject.GetComponent<ItemUI>();
            itemUI.SetSkinSO(skinSO);
        }

    }

    private void Start()
    {
        SkinShop.Instance.OnClick += SkinShop_OnClick;
    }
    private void Update()
    {
        coinAmountText.text = GameManager.instance.GetWallet().coins.ToString();
    }
   private void SkinShop_OnClick()
    {
        DestroyAndSpawnNewPreviewSkin();
    } 


    private void DestroyAndSpawnNewPreviewSkin()
    {
        foreach (Transform items in previewSkinPoint.GetComponentInChildren<Transform>())
        {
            Destroy(items.gameObject);
        }

        GameObject previewGameObject = SkinShop.Instance.GetCurrentSkinSO().skinPrefab;
        Instantiate(previewGameObject, previewSkinPoint);
    }

}
