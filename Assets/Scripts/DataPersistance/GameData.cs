using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int coins;
    public int highScore;
    public List<int> boughtSkinIds;
    public int currentSkinId;
    public Quality currentQuality;

    public GameData(int coins, int highScore, int currentSkinId, List<int> boughtSkinIds)
    {
        this.coins = coins;
        this.highScore = highScore;
        this.currentSkinId = currentSkinId;
        this.boughtSkinIds = boughtSkinIds;
        this.currentQuality = Quality.Medium;
    }
}
