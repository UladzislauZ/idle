using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameField : MonoBehaviour
{
    [SerializeField] private BuildPlace[] _buildPlaces;

    public Player _player;

    public void CollectTax()
    {
        foreach (var buildPlace in _buildPlaces)
        {
            _player.score += buildPlace.GetMoney();
        }
    }

    public void LoadData(BuildPlaceInfo[] infoPlaces)
    {
        for (int i = 0; i < _buildPlaces.Length; i++)
        {
            _buildPlaces[i].LoadInfo(infoPlaces[i]);
        }
    }

    public void SaveData(ref BuildPlaceInfo[] infoPlaces)
    {
        for (int i = 0; i < _buildPlaces.Length; i++)
        {
            _buildPlaces[i].GetInfo(out var level, out var countGive, out var costUpgrade);
            infoPlaces[i].SetInfo(level,countGive,costUpgrade);
        }
    }

    private void OnEnable()
    {
        foreach (var buildPlace in _buildPlaces)
        {
            buildPlace.OnClickUpgrade += UpgradePlace;
        }
    }

    private void OnDisable()
    {
        foreach (var buildPlace in _buildPlaces)
        {
            buildPlace.OnClickUpgrade -= UpgradePlace;
        }
    }

    private void UpgradePlace(string idPlace)
    {
        var place = _buildPlaces.FirstOrDefault(s => s.id == idPlace);
        place.UpgradeBuild(ref _player.score);
    }
}
