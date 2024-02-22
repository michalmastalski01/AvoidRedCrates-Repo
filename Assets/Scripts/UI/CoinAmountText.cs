using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinAmountText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinAmountTextt;

    private float coinAmount;
    private void Start()
    {
        coinAmount = GameManager.instance.GetWallet().coins;
    }
    void Update()
    {
        if(this.enabled)
        {
            coinAmountTextt.text = coinAmount.ToString();
        }
    }
}
