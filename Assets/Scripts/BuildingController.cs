using System;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField] private BuildItem[] _buildItems;

    public void Initialize(GameData gameData)
    {
        var buildingData = gameData.BuildingData;
        for (int i = 0; i < buildingData.Length; i++)
        {
            _buildItems[i].Initialize(buildingData[i].IsUnlock, buildingData[i].UpgradeLevel);
            _buildItems[i].OnProccess += OnProcess;
            _buildItems[i].OnBuildUpgrade += OnBuildUpgrade;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _buildItems.Length; i++)
        {
            if (_buildItems[i] != null)
            {
                _buildItems[i].OnProccess -= OnProcess;
                _buildItems[i].OnBuildUpgrade += OnBuildUpgrade;
            }
        }
    }

    private void OnBuildUpgrade(float value)
    {
        GameManager.Instance.Money -= value;
    }

    private void OnProcess(int value)
    {
        GameManager.Instance.Money += value;
    }

    public BuildingData[] GetBuildingData()
    {
        var data = new BuildingData[_buildItems.Length];
        for (int i = 0; i < _buildItems.Length; i++)
        {
            data[i] = new BuildingData(_buildItems[i].IsUnlock, _buildItems[i].Level);
        }

        return data;
    }
}