using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private RectTransform itemContainer;
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

        SkinShop.Instance.OnClick += SkinShop_OnClick;
    }

    private void Start()
    {
        SkinShop.Instance.OnBuySkin += SkinShop_OnBuySkin;
    }
    private void Update()
    {
        coinAmountText.text = "Coins: " + GameManager.instance.GetWallet().coins;
    }
    private void SkinShop_OnClick()
    {
        DestroyAndSpawnNewPreviewSkin();
    }

    private void SkinShop_OnBuySkin(SkinSO obj)
    {
        coinAmountText.text = "Coins: " + GameManager.instance.GetWallet().coins;
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

    private void DestroyAllObjects()
    {
        if(itemContainer.GetComponentsInChildren<ItemUI>() != null)
        {
            foreach (ItemUI itemUI in itemContainer.GetComponentsInChildren<ItemUI>())
            {
                Destroy(itemUI.gameObject);
            }
        }
    }
}
