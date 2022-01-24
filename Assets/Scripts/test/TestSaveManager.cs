using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSaveManager : MonoBehaviour
{
    private const string DataKey = "TestKey";

    private CharacterData _characterData;

    public CharacterData CharacterData => _characterData;
    
    public void Initialize()
    {
        if (PlayerPrefs.HasKey(DataKey))
        {
            LoadData();
        }
        else
        {
            _characterData = new CharacterData();
        }
    }

    private void LoadData()
    {
        var jsonData = PlayerPrefs.GetString(DataKey);
        Debug.Log(jsonData);
        _characterData = JsonUtility.FromJson<CharacterData>(jsonData);
    }

    public void SaveData()
    {
        var jsonData = JsonUtility.ToJson(_characterData);
        PlayerPrefs.SetString(DataKey, jsonData);
    }
}
