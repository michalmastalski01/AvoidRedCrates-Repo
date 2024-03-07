using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
