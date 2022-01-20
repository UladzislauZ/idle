using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event Action<float> OnMoneyValueChange;
    private SaveSystem _saveSystem;
    private BuildingController _buildingController;
    private UIControllerIDLE _uIControllerIDLE;

    private float _money;
   
    public float Money
    {
        get => _money;
        set
        {
            if (value < 0)
                return;
            if (value == _money)
                return;

            _money = value;
            _money = (float)Math.Round(_money, 2);
            OnMoneyValueChange?.Invoke(_money);
        }
    }
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _saveSystem = GetComponent<SaveSystem>();
        _buildingController = GetComponentInChildren<BuildingController>();
        _uIControllerIDLE = FindObjectOfType<UIControllerIDLE>();
        _saveSystem.Initialize();
        _money = _saveSystem.GameData.Money;
        _buildingController.Initialize(_saveSystem.GameData);
        _uIControllerIDLE.SwitchPanels(true);
    }
    private void OnApplicationFocus(bool focus)
    {
        if(!focus)
        {
            SaveData();
        }
    }

    private void SaveData()
    {
        _saveSystem.GameData.Money = _money;
        _saveSystem.GameData.BuildingData = _buildingController.GetBuildingData(); ; //TODO: add logic
        _saveSystem.SaveData();
    }
    
    public void StartGame()
    {
        _uIControllerIDLE.SwitchPanels(false);
    }
}
