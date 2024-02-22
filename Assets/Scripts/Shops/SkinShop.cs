using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkinShop : MonoBehaviour
{
    public static SkinShop Instance { get; private set; }
    public enum RarityLevel
    {
        Common,
        Uncommon,
        Rare,
        UltraRare
    }

    public event Action<SkinSO> OnBuySkin;
    public event Action OnClick;

    [SerializeField] private GameObject itemContainer;
    [SerializeField] private List<SkinSO> skinSOList;

    private SkinSO currentSkinSO;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        foreach (SkinSO skin in skinSOList)
        {
            if (skin.id == GameManager.instance.GetWallet().currentSkinId)
            {
                SetCurrentSkinSO(skin);
                //Debug.Log(currentSkinSO.skinName);
            }
        }
    }
    public void BuySkin()
    {
        Wallet wallet = GameManager.instance.GetWallet();
        if (wallet.coins >= currentSkinSO.skinCost)
        {
            if (!wallet.boughtSkinsList.Contains(GetCurrentSkinSO()))
            {
                SoundManager.Instance.PlayClickSound();
                wallet.SubtractCoins(currentSkinSO.skinCost);
                wallet.AddSkin(currentSkinSO);
                OnBuySkin?.Invoke(GetCurrentSkinSO());
            }
        }
    }

    public SkinSO GetCurrentSkinSO()
    {
        return currentSkinSO;
    }
    public void SetCurrentSkinSO(SkinSO skinSO)
    {
        currentSkinSO = skinSO;
        SoundManager.Instance.PlayClickSound();
        OnClick?.Invoke();
    }

    public List<SkinSO> GetSkinSOList()
    {
        return skinSOList;
    }



}
