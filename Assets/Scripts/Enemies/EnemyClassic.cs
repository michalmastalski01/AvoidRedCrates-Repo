using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClassic : Enemy
{
    [SerializeField] private float movingSpeed;


    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * movingSpeed * Time.deltaTime;

        if (transform.position.z > 15f)
        {
            DestroyEnemy();
        }
    }
}
