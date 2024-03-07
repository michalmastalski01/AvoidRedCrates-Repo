using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour,  IDataPersistence
{
    public int coins { get; private set; }

    public List<SkinSO> boughtSkinsList { get; private set; } = new List<SkinSO>();

    public List<UpgradeSO> boughtUpgradesList { get; private set; } = new List<UpgradeSO>();

    public int highScore { get; private set; }

    public int currentSkinId { get; private set; }

    public bool isMusicOn { get; private set; }
    public bool isSoundOn { get; private set; }


    public void AddCoins(int coinsToAdd)
    {
        coins += coinsToAdd;
    }
    public void SetCoins(int coinsToSet)
    {
        coins = coinsToSet;
    }
    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
    }
    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
    }

    public void SubtractCoins(int coinsToSubtract)
    {
        coins -= coinsToSubtract;
    }
    public void AddSkin(SkinSO skinSO)
    {
        boughtSkinsList.Add(skinSO);
    }
    public void AddUpgrade(UpgradeSO upgradeSO)
    {
        boughtUpgradesList.Add(upgradeSO);
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

    public void LoadData(GameData gameData)
    {
        SetCoins(gameData.coins);
        SetCurrentSkinId(gameData.currentSkinId);
        TrySetHighScore(gameData.highScore);
        isMusicOn = gameData.isMusicOn;
        isSoundOn = gameData.isSoundOn;

        List<SkinSO> skinList = SkinShop.Instance.GetSkinSOList();
        foreach (SkinSO skinSO in skinList)
        {
            if (gameData.boughtSkinIds.Contains(skinSO.id))
            {
                AddSkin(skinSO);
            }
        }

        List<UpgradeSO> upgradeList = UpgradeShop.Instance.GetUpgradeSOList();
        foreach (UpgradeSO upgradeSO in upgradeList)
        {
            if (gameData.boughtUpgradeIds.Contains(upgradeSO.id))
            {
                AddUpgrade(upgradeSO);
            }
        }
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.coins = coins;
        gameData.highScore = highScore;
        gameData.currentSkinId = currentSkinId;
        gameData.boughtSkinIds.Clear();
        gameData.boughtUpgradeIds.Clear();
        gameData.isMusicOn = isMusicOn;
        gameData.isSoundOn = isSoundOn;

        foreach (SkinSO skinSO in boughtSkinsList)
        {
            gameData.boughtSkinIds.Add(skinSO.id);
        }
        foreach (UpgradeSO upgradeSO in boughtUpgradesList)
        {
            gameData.boughtUpgradeIds.Add(upgradeSO.id);
        }
    }
}
