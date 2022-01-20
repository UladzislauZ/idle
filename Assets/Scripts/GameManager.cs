using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst { get; private set; }
    public event Action<float> OnMoneyValueChange;

    private SaveSystem _saveSystem;
    private float _money;
    private BuildingController _buildingController;
    private UIController _uiController;

    public float Money
    {
        get => _money;
        set
        {
            if (value < 0 || value == _money) return;
            
            _money = value;
            _money = (float) Math.Round(_money, 2);
            OnMoneyValueChange?.Invoke(_money);
        }
    }
    
    private void Awake()
    {
        if (Inst != null)
        {
            Destroy(gameObject);
            return;
        }
        Inst = this;

        _uiController = FindObjectOfType<UIController>();
        _buildingController = FindObjectOfType<BuildingController>();
        _saveSystem = GetComponent<SaveSystem>();
        _saveSystem.Initialize();
        _money = _saveSystem.GameData.Money;
        _buildingController.Initialize(_saveSystem.GameData);
        _uiController.SwitchPanel(true);
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            SaveData();
        }
    }

    private void SaveData()
    {
        _saveSystem.GameData.Money = _money;
        _saveSystem.GameData.BuildingDatas = _buildingController.GetBuildingData();
        _saveSystem.SaveData();
    }

    public void StartGame()
    {
        _uiController.SwitchPanel(false);
    }

    public void PauseGame()
    {
        _uiController.SwitchPanel(true);
    }
}
