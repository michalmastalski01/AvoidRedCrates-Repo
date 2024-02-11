using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI rarityText;
    [SerializeField] private Image itemBackground;
    [SerializeField] private Image rarityUnderlineColorBar;
    [SerializeField] private GameObject highlightBorder;
    [SerializeField] private Color boughtColor;

    private UpgradeSO upgradeSO;
    private void Awake()
    {
        UpgradeShop.Instance.OnClick += OnClickEvent;
    }
    private void Start()
    {
        if (GameManager.instance.GetWallet().boughtUpgradesList.Contains(upgradeSO))
        {
            itemBackground.color = boughtColor;
        }
        if (upgradeSO != null)
        {
            itemText.text = upgradeSO.upgradeName;
            costText.text = upgradeSO.upgradeCost.ToString();
        }
        UpgradeShop.Instance.OnBuyUpgrade += OnBuySkinEvent;
    }

    public void SetUpgradeSO(UpgradeSO upgradeSO)
    {
        this.upgradeSO = upgradeSO;
    }
    private void OnClickEvent()
    {
        HighlightItemUI();
    }
    private void OnBuySkinEvent(UpgradeSO currentUpgradeSO)
    {
        if (currentUpgradeSO == upgradeSO)
        {
            itemBackground.color = boughtColor;
        }
    }

    private void HighlightItemUI()
    {
        if (UpgradeShop.Instance.GetCurrentUpgradeSO() == upgradeSO)
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
        UpgradeShop.Instance.SetCurrentUpgradeSO(upgradeSO);
    }
}
