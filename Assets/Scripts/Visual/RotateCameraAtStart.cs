using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraAtStart : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 gameplayPosition;
    [SerializeField] private Vector3 startRotation;
    [SerializeField] private Vector3 gameplayRotation;
    [SerializeField] private float transitionTime = 10f;
    private bool isPlaying = false;

    private float timer = 0;
    private void Start()
    {
        GameManager.instance.OnStart += GameManager_OnStart;
        transform.position = startPosition;
        transform.rotation = new Quaternion(startRotation.x, startRotation.y, startRotation.z, 1);
    }
    private void Update()
    {
        if (isPlaying)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, gameplayPosition, timer * transitionTime);
            transform.eulerAngles = Vector3.Lerp(startRotation, gameplayRotation, timer * transitionTime);

            if (timer >= 1)
            {
                isPlaying = false;
            }
        }
    }
    private void GameManager_OnStart()
    {
        isPlaying = true;
    }
}
