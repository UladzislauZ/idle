using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    private const int BuildingCount = 4;
    public float Money = 100;
    public BuildingData[] BuildingData;
     
    public GameData()
    {
        BuildingData = new BuildingData[BuildingCount];
        for (int i = 0; i < BuildingCount; i++)
        {
            BuildingData[i] = new BuildingData();
        }
    }

}
