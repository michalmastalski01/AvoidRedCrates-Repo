using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IsInteractableBuyButton : MonoBehaviour
{
    [SerializeField] private bool isSkinButton;
    private Button button;
    private TextMeshProUGUI buttonText;

    private float transparency = 0.5f;
    // Start is called before the first frame update
    private void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        button = GetComponent<Button>();
        button.interactable = false;
        buttonText.color = new Color(buttonText.color.r, buttonText.color.g, buttonText.color.b, transparency);
        SkinShop.Instance.OnClick += SkinShop_OnClick;
        UpgradeShop.Instance.OnClick += UpgradeShop_OnClick;
    }

    private void SkinShop_OnClick()
    {
        if(isSkinButton)
        {
            Wallet wallet = GameManager.instance.GetWallet();
            if (wallet.coins >= SkinShop.Instance.GetCurrentSkinSO().skinCost)
            {
                if (!wallet.boughtSkinsList.Contains(SkinShop.Instance.GetCurrentSkinSO()))
                {
                    button.interactable = true;
                    buttonText.color = new Color(buttonText.color.r, buttonText.color.g, buttonText.color.b, 1f);
                    return;
                }
            }
            button.interactable = false;
            buttonText.color = new Color(buttonText.color.r, buttonText.color.g, buttonText.color.b, transparency);
        }
    }
    private void UpgradeShop_OnClick()
    {
        if(!isSkinButton)
        {
            Wallet wallet = GameManager.instance.GetWallet();
            if (wallet.coins >= UpgradeShop.Instance.GetCurrentUpgradeSO().upgradeCost)
            {
                if (!wallet.boughtUpgradesList.Contains(UpgradeShop.Instance.GetCurrentUpgradeSO()))
                {
                    button.interactable = true;
                    buttonText.color = new Color(buttonText.color.r, buttonText.color.g, buttonText.color.b, 1f);
                    return;
                }
            }
            button.interactable = false;
            buttonText.color = new Color(buttonText.color.r, buttonText.color.g, buttonText.color.b, transparency);
        }
    }
}
