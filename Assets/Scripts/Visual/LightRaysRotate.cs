using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRaysRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    void Update()
    {
        if(this.enabled)
        {
            transform.eulerAngles += new Vector3(0, 0, Time.deltaTime * rotationSpeed);
        }
    }
}
