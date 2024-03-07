using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using UnityEditor.Rendering;

public class SkinShopUI : ShopUI
{
    [SerializeField] private TextMeshProUGUI coinAmountText;
    [SerializeField] private Transform previewSkinPoint;
    [SerializeField] private GameObject boughtSkinScreen;
    [SerializeField] private TextMeshProUGUI boughSkinNameText;
    [SerializeField] private float boughtNewSkinScreenDuration;
    [SerializeField] private float transitionTime;

    private State currentBoughtSkinState;

    private bool isPlayerBoughtSkin;
    private SkinSO boughtSkinSO;

    private float timer;
    private enum State
    {
        Start,
        Visible,
        Exit
    }
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
        DestroyAndSpawnNewPreviewSkin();
        boughtSkinScreen.SetActive(false);
        boughtSkinScreen.transform.localScale = Vector3.zero;
        isPlayerBoughtSkin = false;
        currentBoughtSkinState = State.Start;
        timer = 0.5f;

        SkinShop.Instance.OnClick += SkinShop_OnClick;
        SkinShop.Instance.OnBuySkin += SkinShop_OnBuySkin;
    }

    private void SkinShop_OnBuySkin(SkinSO obj)
    {
        boughtSkinSO = obj;
        isPlayerBoughtSkin = true;
    }

    private void Update()
    {
        coinAmountText.text = GameManager.instance.GetWallet().coins.ToString();

        if (isPlayerBoughtSkin)
        {
            BoughtState();
        }
    }

    private void BoughtState()
    {
        switch (currentBoughtSkinState)
        {
            case State.Start:
                boughtSkinScreen.SetActive(true);
                boughSkinNameText.text = boughtSkinSO.skinName;
                boughtSkinScreen.transform.localScale += Vector3.Slerp(Vector3.zero, Vector3.one, Time.deltaTime * transitionTime);

                if(boughtSkinScreen.transform.localScale.x >= 1f)
                {
                    timer = boughtNewSkinScreenDuration;
                    currentBoughtSkinState++;
                }
                break;
            case State.Visible:
                timer -= Time.deltaTime;
                if(timer < 0)
                {
                    currentBoughtSkinState++;
                }
                break;
            case State.Exit:
                boughtSkinScreen.transform.localScale -= Vector3.Slerp(Vector3.zero, Vector3.one, Time.deltaTime * transitionTime);

                if (boughtSkinScreen.transform.localScale.x <= 0f)
                {
                    boughtSkinScreen.transform.localScale = Vector3.zero;
                    boughtSkinScreen.SetActive(false);
                    isPlayerBoughtSkin = false;
                    currentBoughtSkinState = State.Start;
                }
                break;
        }
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
