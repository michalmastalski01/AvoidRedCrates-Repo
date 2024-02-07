using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateCharacter : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 10f;
    void Update()
    {
        Transform childTransform = GetComponentInChildren<Transform>();
        childTransform.eulerAngles += new Vector3(0, Time.deltaTime * rotateSpeed, 0);
    }
}
