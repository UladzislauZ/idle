using System.Collections;
using System.Collections.Generic;
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

        }
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
