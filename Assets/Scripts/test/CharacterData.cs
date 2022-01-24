using System;
using UnityEngine;

[Serializable]
public class CharacterData
{
    public int State = 0;
    public float StateTime = 0f;
    public Vector3 position;
    
    public CharacterData()
    {
        position = Vector3.zero;
    }

    public CharacterData(int state, float time, Vector3 pos)
    {
        State = state;
        StateTime = time;
        position = pos;
    }
}
