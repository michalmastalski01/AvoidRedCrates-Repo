using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float movingSpeed;



    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * movingSpeed * Time.deltaTime;

        if (transform.position.z > 15f)
        {
            Disable();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.GameOver();
            gameObject.SetActive(false);
        }
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
