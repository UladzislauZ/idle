using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingItemsContainer", menuName = "Configs/BuildingItemsContainer")]
public class BuildingItemsContainer : ScriptableObject
{
    [SerializeField] private float _unlockPrice;
    [SerializeField] private float _startUpgradePrice;
    [SerializeField] private float _priceMultiplier;
    [SerializeField] private BuildingItemConfig[] _buildingItemConfigs;

    public float UnlockPrice => _unlockPrice;

    public float StartUpgradePrice => _startUpgradePrice;

    public float PriceMultiplier => _priceMultiplier;

    public bool IsUpgradeExist(int index)
    {
        return index >= 0 && index < _buildingItemConfigs.Length;
    }

    public BuildingItemConfig GetUpgrade(int index)
    {
        return _buildingItemConfigs[index];
    }
    
    
    
}
