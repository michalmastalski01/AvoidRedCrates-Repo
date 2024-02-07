using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour,  IDataPersistence
{
    public int coins { get; private set; }

    public List<SkinSO> boughtSkinsList { get; private set; } = new List<SkinSO>();

    public int highScore { get; private set; }

    public int currentSkinId { get; private set; }


    public void AddCoins(int coinsToAdd)
    {
        coins += coinsToAdd;
    }
    public void SetCoins(int coinsToSet)
    {
        coins = coinsToSet;
    }
    public void SubtractCoins(int coinsToSubtract)
    {
        coins -= coinsToSubtract;
    }
    public void AddSkin(SkinSO skinSO)
    {
        boughtSkinsList.Add(skinSO);
    }
    public bool TrySetHighScore(int score)
    {
        if(score > highScore)
        {
            highScore = score;
            return true;
        }
        return false;
    }
    public void SetCurrentSkinId(int newCurrentSkinId)
    {
        currentSkinId = newCurrentSkinId;
    }
    private void ClearBoughtSkinList()
    {
        boughtSkinsList.Clear();
    }
    public void ShowListOfBoughtSkins()
    {
        foreach (SkinSO skinSO in boughtSkinsList)
        {
            //Debug.Log(skinSO.skinName);
        }
    }

    public void LoadData(GameData gameData)
    {
        SetCoins(gameData.coins);
        SetCurrentSkinId(gameData.currentSkinId);
        TrySetHighScore(gameData.highScore);

        List<SkinSO> skinList = SkinShop.Instance.GetSkinSOList();
        foreach (SkinSO skinSO in skinList)
        {
            if (gameData.boughtSkinIds.Contains(skinSO.id))
            {
                AddSkin(skinSO);
            }
            if (gameData.currentSkinId == skinSO.id)
            {
                SkinShop.Instance.SetCurrentSkinSO(skinSO);
            }
        }
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.coins = coins;
        gameData.highScore = highScore;
        gameData.currentSkinId = currentSkinId;
        gameData.boughtSkinIds.Clear();

        foreach (SkinSO skinSO in boughtSkinsList)
        {
            gameData.boughtSkinIds.Add(skinSO.id);
        }
    }
}
