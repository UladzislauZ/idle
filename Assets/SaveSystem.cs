using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private GameData _gameData;
    private const string DataKey = "GameData";
    public GameData GameData => _gameData;
    public void Initialize()
    {
       if(PlayerPrefs.HasKey(DataKey))
        {
            LoadData();
        }
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
}
