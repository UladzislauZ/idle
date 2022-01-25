using UnityEngine;

[CreateAssetMenu(fileName = "BuildingItemConfig", menuName = "Config/BuildingItemConfig")]
public class BuildingItemConfig : ScriptableObject
{
    [SerializeField] private GameObject _model;
    [SerializeField] private int _processCof;

    public GameObject Model => _model;
    public int ProcessCof => _processCof;
}
