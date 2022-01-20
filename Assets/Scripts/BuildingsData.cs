using System;

[Serializable]
public class BuildingData
{
    public bool lockState;
    public int UpgradeLevel = 0;

    public BuildingData()
    {
        lockState = false;
        UpgradeLevel = 0;
    }

    public BuildingData(bool lockState, int level)
    {
        this.lockState = lockState;
        UpgradeLevel = level;
    }
}
