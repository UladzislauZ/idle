using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private const string DataKey = "GameData";

    private GameData _gameData;
    public GameData GameData => _gameData;
    private static string PathJson = "";
    private static string PathBin = "";
    public void Initialize()
    {
        PathJson = System.IO.Path.Combine(Application.persistentDataPath + $"{DataKey}.json");
        PathBin = System.IO.Path.Combine(Application.persistentDataPath + $"{DataKey}.data");

        if(PlayerPrefs.HasKey(DataKey))
         {
             LoadDataJson();
         }
        /* if (System.IO.File.Exists(PathJson))
         {
             LoadDataJson();
         }
        */
        /* if (System.IO.File.Exists(PathBin))
         {
             LoadDataBin();
         }*/
        else
        {
            _gameData = new GameData();
        }
    }

    private void LoadData()
    {
        var jsonData = PlayerPrefs.GetString(DataKey);
        _gameData = JsonUtility.FromJson<GameData>(jsonData);
    }

    public void SaveData()
    {
        var jsonData = JsonUtility.ToJson(_gameData);
        PlayerPrefs.SetString(DataKey, jsonData);
    }
    private void LoadDataJson()
    {
        var jsonData = System.IO.File.ReadAllText(PathJson); ;
        _gameData = JsonUtility.FromJson<GameData>(jsonData);
    }

    public void SaveDataJson()
    {
        var jsonData = JsonUtility.ToJson(_gameData);
        System.IO.File.WriteAllText(PathJson, jsonData);
    }

    private void LoadDataBin()
    {
        var dataStream = new System.IO.FileStream(PathBin, System.IO.FileMode.Open);
        var converter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        _gameData = converter.Deserialize(dataStream) as GameData;
        dataStream.Close();
    }

    public void SaveDataBin()
    {
        var dataStream = new System.IO.FileStream(PathBin, System.IO.FileMode.OpenOrCreate);
        var converter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        converter.Serialize(dataStream, _gameData);
        dataStream.Close();

    }
}
