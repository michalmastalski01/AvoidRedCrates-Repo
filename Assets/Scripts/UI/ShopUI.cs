using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShopUI : MonoBehaviour
{
    [SerializeField] protected GameObject itemPrefab;
    [SerializeField] protected RectTransform itemContainer;

    protected void DestroyAllObjects()
    {
        if (itemContainer.GetComponentsInChildren<ItemUI>() != null)
        {
            foreach (ItemUI itemUI in itemContainer.GetComponentsInChildren<ItemUI>())
            {
                Destroy(itemUI.gameObject);
            }
        }
    }
}
