using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int _score;
    public BuildPlaceInfo[] _levelsBuilds = new BuildPlaceInfo[4];

    public SaveData()
    {
        _score = 0;
        for (int i = 0; i < _levelsBuilds.Length; i++)
        {
            _levelsBuilds[i] = new BuildPlaceInfo();
        }
    }
}