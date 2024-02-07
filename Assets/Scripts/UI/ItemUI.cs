using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI rarityText;
    [SerializeField] private Image itemBackground;
    [SerializeField] private Image rarityUnderlineColorBar;
    [SerializeField] private GameObject highlightBorder;
    [SerializeField] private Color boughtColor;
    
    private SkinSO skinSO;
    private void Awake()
    {
        SkinShop.Instance.OnClick += SkinShop_OnClick;
    }
    private void Start()
    {
        if (GameManager.instance.GetWallet().boughtSkinsList.Contains(skinSO))
        {
            itemBackground.color = boughtColor;
        }
        if(skinSO != null)
        {
            itemText.text = skinSO.skinName;
            costText.text = skinSO.skinCost.ToString();
        }
        SetRarityColor();

        SkinShop.Instance.OnBuySkin += SkinShop_OnBuySkin;
    }

    public void SetSkinSO(SkinSO skinSO)
    {
        this.skinSO = skinSO;
    }

    private void SkinShop_OnClick()
    {
        HighlightItemUI();
    }

    private void SkinShop_OnBuySkin(SkinSO currentSkinSO)
    {
        if(currentSkinSO == skinSO)
        {
            itemBackground.color = boughtColor;
        }
    }

    private void SetRarityColor()
    {
        switch (skinSO.rarityLevel)
        {
            case Rarity.RarityLevel.Common:
                rarityText.text = Rarity.RarityLevel.Common.ToString();
                rarityUnderlineColorBar.color = Color.gray;
                break;
            case Rarity.RarityLevel.Uncommon:
                rarityText.text = Rarity.RarityLevel.Uncommon.ToString();
                rarityUnderlineColorBar.color = Color.blue;
                break;
            case Rarity.RarityLevel.Rare:
                rarityText.text = Rarity.RarityLevel.Rare.ToString();
                rarityUnderlineColorBar.color = Color.magenta;
                break;
            case Rarity.RarityLevel.UltraRare:
                rarityText.text = Rarity.RarityLevel.UltraRare.ToString();
                rarityUnderlineColorBar.color = Color.yellow;
                break;

        }
    }

    private void HighlightItemUI()
    {
        if(SkinShop.Instance.GetCurrentSkinSO() == skinSO)
        {
            highlightBorder.SetActive(true);
        }
        else
        {
            highlightBorder.SetActive(false);
        }
    }

    public void OnClick()
    {
        SkinShop.Instance.SetCurrentSkinSO(skinSO);
    }
}
