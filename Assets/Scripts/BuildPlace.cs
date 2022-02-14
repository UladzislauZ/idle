using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.Scripts;
using UnityEngine.UI;

public class BuildPlace : MonoBehaviour
{
    public string id { get; private set; }
    [SerializeField] private string _id;
    [SerializeField] private GameObject[] _buildPrefabs;
    [SerializeField] private GameObject _parent;
    [SerializeField] private GameObject _currentModelBuild;
    [SerializeField] private TextMeshProUGUI _costLabel;
    [SerializeField] private TextMeshProUGUI _countGiveLabel;
    [SerializeField] private GameObject _errorLabel;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Animation _animationError;

    private BuildPlaceInfo _info = new BuildPlaceInfo();

    public event Action<string> OnClickUpgrade;

    private void OnEnable()
    {
        id = _id;
        _upgradeButton.onClick.AddListener(ClickUpgrade);
    }

    private void OnDisable()
    {
        _upgradeButton.onClick.RemoveListener(ClickUpgrade);
    }

    public int GetMoney() => _info.GetlevelCountGive();

    public void GetInfo(out int level, out int countGive, out int costUpgrade)
    {
        level = _info.GetlevelBuild();
        countGive = _info.GetlevelCountGive();
        costUpgrade = _info.GetlevelCostUpgrade();
    }

    public void LoadInfo(BuildPlaceInfo info)
    {
        _info.SetInfo(info.GetlevelBuild(), info.GetlevelCountGive(), info.GetlevelCostUpgrade());
        DrowToScreen();
    }

    private void DrowToScreen()
    {
        if (_currentModelBuild != null)
        {
            Destroy(_currentModelBuild);
        }

        _currentModelBuild = Instantiate(_buildPrefabs[_info.GetlevelBuild()]);
        _currentModelBuild.transform.parent = _parent.transform;
        _currentModelBuild.transform.localPosition = Vector3.zero;
        _currentModelBuild.transform.localRotation = _parent.transform.localRotation;
        _currentModelBuild.transform.localScale *= 13;

        _costLabel.text = $"{_info.GetlevelCostUpgrade()}$";
        _countGiveLabel.text = $"+ {_info.GetlevelCountGive()}$";
    }

    public void ClickUpgrade()
    {
        OnClickUpgrade?.Invoke(_id);
    }

    public bool UpgradeBuild(ref int count)
    {
        if (IsPossibleUpgrade(count)) 
        {
            count -= _info.GetlevelCostUpgrade();
            _info.Upgrade();
            DrowToScreen();
            return true;
        }
        else
        {
            switch (id)
            {
                case "1": _animationError.Play("Error");
                    break;
                case "2":
                    _animationError.Play("Error 1");
                    break;
                case "3":
                    _animationError.Play("Error 2");
                    break;
                case "4":
                    _animationError.Play("Error 3");
                    break;
            }
            return false;
        }
    }

    public bool IsPossibleUpgrade(int count)
    {
        return count > _info.GetlevelCostUpgrade();
    }


}
