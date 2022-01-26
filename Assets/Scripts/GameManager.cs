using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event Action<float> OnMoneyValueChange;
    private SaveSystem _saveSystem;
    private BuildingController _buildingController;
    private UiController _uiController;

    private float _money;

    public float Money
    {
        get => _money;
        set
        {
            if (value < 0)
                return;

            _money = value;
            _money = (float) Math.Round(_money, 2);
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
        _uiController = FindObjectOfType<UiController>();
        _saveSystem.Initialize();
        _money = _saveSystem.GameData.Money;
    }

    private void Start()
    {
        _buildingController.Initialize(_saveSystem.GameData);
        _uiController.SwitchPanels(true);
        _money = _saveSystem.GameData.Money;
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
        _saveSystem.GameData.BuildingData = _buildingController.GetBuildingData();
        _saveSystem.SaveData();
    }

    public void StartGame()
    {
        _uiController.SwitchPanels(false);
    }
}