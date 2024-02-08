using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSMeter : MonoBehaviour
{
    private TextMeshProUGUI fpsMeterText;
    private float refreshRate = 0.2f;

    private void Start()
    {
        fpsMeterText = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        refreshRate -= Time.deltaTime;

        if(refreshRate < 0)
        {
            fpsMeterText.text = ((int)(1f / Time.deltaTime)).ToString();
            refreshRate = 0.2f;
        }
    }
}
