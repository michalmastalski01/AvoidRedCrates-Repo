using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTab : MonoBehaviour
{
    [SerializeField] ShopTabsHandler shopTabsHandler;
    [SerializeField] Image backgroundActive;
    [SerializeField] List<GameObject> contentToShow;
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
            backgroundActive.color = new Color(255, 255, 255, 0.25f);
        }
        else
        {
            foreach (GameObject content in contentToShow)
            {
                content.SetActive(false);
            }
            backgroundActive.color = new Color(255, 255, 255, 0f);
        }
    }
}
