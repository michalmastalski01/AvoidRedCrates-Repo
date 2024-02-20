using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSMeter : MonoBehaviour
{
    [SerializeField] private float refreshRate;
    private TextMeshProUGUI fpsMeterText;
    private float rate;

    private void Start()
    {
        rate = refreshRate;
        fpsMeterText = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        rate -= Time.deltaTime;

        if(rate < 0)
        {
            fpsMeterText.text = ((int)(1f / Time.deltaTime)).ToString();
            rate = refreshRate;
        }
    }
}
