using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopTab : MonoBehaviour
{
    [SerializeField] ShopTabsHandler shopTabsHandler;
    [SerializeField] Image backgroundActive;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] List<GameObject> contentToShow;
    [SerializeField] private Color backgroundColor;
    [SerializeField] private Color enableTextColor;
    [SerializeField] private Color disableTextColor;
    private void Awake()
    {
        shopTabsHandler.OnTabClick += ShopTabsHandler_OnTabClick;
    }

    private void ShopTabsHandler_OnTabClick()
    {
        if(shopTabsHandler.GetCurrentOpenTab() == this)
        {
            foreach(GameObject content in contentToShow)
            {
                content.SetActive(true);
            }
            backgroundActive.color = backgroundColor;
            text.color = enableTextColor;
        }
        else
        {
            foreach (GameObject content in contentToShow)
            {
                content.SetActive(false);
            }
            backgroundActive.color = new Color(255, 255, 255, 0f);
            text.color = disableTextColor;
        }
    }
}
