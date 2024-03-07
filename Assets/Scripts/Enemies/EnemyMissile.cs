using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : Enemy
{
    [SerializeField] private AudioClip explosionAudioClip;
    [SerializeField] private float time;
 
    private float timer = 5f;

    private bool boom;
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= time && !boom)
        {
            SoundManager.Instance.PlaySound(explosionAudioClip);
            boom = true;
        }

        if(timer < 0)
        {
            DestroyEnemy();
        }
    }
}
