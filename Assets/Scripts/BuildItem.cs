using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildItem : MonoBehaviour
{
    public bool IsUnlocked { get; private set; }
    public int Level { get; private set; }

    public void Initialize(bool unlockState, int level)
    {
        IsUnlocked = unlockState;
        Level = level;
    }
}
