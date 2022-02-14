using Firebase.Storage;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager _ui = null;
    [SerializeField] private Player _player = new Player("Bob Marley");
    [SerializeField] private GameObject _levelPrefab;

    private GameField _gameField;

    private bool onPlay;

    private void OnEnable()
    {
        _ui.LoadGame();
        _ui.OnLoadLevel += StartLevel;
        _ui.OnSave += SaveGame;
    }

    private void OnApplicationQuit()
    {
        _ui.OnLoadLevel -= StartLevel;
        _ui.OnSave -= SaveGame;
        StopCoroutine(Play());
    }

    private IEnumerator Play()
    {
        while (onPlay)
        {
            _gameField.CollectTax();
            _ui.PrintScore(_player.score.ToString());
            yield return new WaitForSecondsRealtime(1);
        }
    }

    private void StartLevel()
    {
        _gameField = Instantiate(_levelPrefab).GetComponent<GameField>();
        _gameField._player = _player;
        LoadGame();
        onPlay = true;
        StartCoroutine(Play());
        //Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLevelStart);
    }

    private void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/DataMining4.dat");
        SaveData data = new SaveData();
        data._score = _player.score;
        _gameField.SaveData(ref data._levelsBuilds);
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
        //Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLevelEnd);
    }

    private void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/DataMining4.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/DataMining4.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            _player.score = data._score;
            _gameField.LoadData(data._levelsBuilds);
            Debug.Log("Game data loaded!");
            //Firebase.Analytics.FirebaseAnalytics.LogEvent("LoadGame", "progress", _player.score);
        }
        else
        {
            Debug.LogError("There is no save data!");
            var bpis = new Assets.Scripts.BuildPlaceInfo[4];
            for(var i=0;i<bpis.Length;i++)
            {
                bpis[i] = new Assets.Scripts.BuildPlaceInfo();
            }

            _gameField.LoadData(bpis);
            //Firebase.Analytics.FirebaseAnalytics.LogEvent("New game");
        }
    }
}
