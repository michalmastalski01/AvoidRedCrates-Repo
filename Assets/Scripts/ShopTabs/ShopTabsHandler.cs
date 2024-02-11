using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopTabsHandler : MonoBehaviour
{
    [SerializeField] List<ShopTab> shopTabs;

    private ShopTab currentOpenTab;

    public event Action OnTabClick;

    private void Awake()
    {
        currentOpenTab = shopTabs[0];
    }
    private void Start()
    {
        OnTabClick?.Invoke();
    }
    public void SetCurrentOpenTab(ShopTab shopTab)
    {
        currentOpenTab = shopTab;
        OnTabClick?.Invoke();
    }
    public ShopTab GetCurrentOpenTab()
    {
        return currentOpenTab;
    }
}
