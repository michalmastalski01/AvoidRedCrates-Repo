using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling Instance { get; private set; }

    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }


}
