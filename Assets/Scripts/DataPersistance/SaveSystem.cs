using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveData(int coins, int highScore, int currentSkinId, List<SkinSO> boughtSkinsList)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "gamedata2.save";

        List<int> boughtSkinIds = new List<int>();
        foreach (SkinSO skin in boughtSkinsList)
        {
            int boughtSkinId = skin.id;

            boughtSkinIds.Add(boughtSkinId);
        }
        FileStream stream = new FileStream(path, FileMode.Create);
        GameData gameData = new GameData(coins, highScore, currentSkinId, boughtSkinIds);

        binaryFormatter.Serialize(stream, gameData);
        stream.Close();
    }
    public static GameData LoadData()
    {
        string path = Application.persistentDataPath + "gamedata2.save";
        if (!File.Exists(path))
        {
            return null;
        }
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        try
        {
            GameData gameData = binaryFormatter.Deserialize(stream) as GameData;
            stream.Close();
            return gameData;
        }
        catch
        {
            Debug.LogErrorFormat("Failed to load file!");
            stream.Close();
            return null;
        }
    }
}
