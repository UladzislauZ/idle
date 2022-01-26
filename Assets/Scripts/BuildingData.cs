using System;

[Serializable]
public class BuildingData
{
    public bool IsUnlock;
    public int UpgradeLevel = 0;

    public BuildingData()
    {
        IsUnlock = false;
        UpgradeLevel = 0;
    }

    public BuildingData(bool isUnlock, int upgradeLevel)
    {
        IsUnlock = isUnlock;
        UpgradeLevel = upgradeLevel;
    }
}