using UnityEngine;

[CreateAssetMenu(fileName = "BuildingItemConfig", menuName = "Configs/BuildingItemConfig")]
public class BuildingItemConfig : ScriptableObject
{
    [SerializeField] private GameObject _model;
    [SerializeField] private int _processResult;

    public GameObject Model => _model;
    public int ProcessResult => _processResult;
}
