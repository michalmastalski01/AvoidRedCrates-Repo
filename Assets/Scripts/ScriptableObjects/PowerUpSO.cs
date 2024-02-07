using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PowerUpSO : ScriptableObject
{
    public string powerUpName;
    public Sprite powerUpSprite;
    public Color powerUpColor;
    public PowerUp.PowerUpType powerUpType;
    public float powerUpDuration;
}
