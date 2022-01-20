using System;
using UnityEngine;

[Serializable]
public class GameData
{
    private const int BuildingsCount = 4;
    
    public float Money = 100f;
    public BuildingData[] BuildingDatas;

    public GameData()
    {
        BuildingDatas = new BuildingData[BuildingsCount];
        for (int i = 0; i < BuildingsCount; i++)
        {
            BuildingDatas[i] = new BuildingData();
        }
    }

}