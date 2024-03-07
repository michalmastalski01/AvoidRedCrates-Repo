using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyQuick : Enemy
{
    [SerializeField] private float movingSpeed;

    void Update()
    {
        transform.position += transform.forward * movingSpeed * Time.deltaTime;

        if (transform.position.z > 15f)
        {
            DestroyEnemy();
        }
    }
}
