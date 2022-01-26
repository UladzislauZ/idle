using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class BuildItem : MonoBehaviour
{
    [SerializeField] private BuildingItemsContainer _itemsContainer;
    [SerializeField] private Transform _modelPoint;
    
    [SerializeField] private BuildingButtonController _buttonController;
    private GameObject _currentModel;
    private Coroutine _timerCoroutine;
    public bool IsUnlock { get; private set; }
    public int Level { get; private set; }

    public event Action<int> OnProccess;
    public event Action<float> OnBuildUpgrade; 

    private void Awake()
    {
        _buttonController = GetComponentInChildren<BuildingButtonController>(true);
    }

    public void Initialize(bool isUnlock, int level)
    {
        IsUnlock = isUnlock;
        Level = level;
        GetComponentInChildren<Canvas>().worldCamera = Camera.main;
        if (isUnlock && Level >= 0) SetModel(Level);
        UpdateButtonState();
        GameManager.Instance.OnMoneyValueChange += _buttonController.OnMoneyValueChange;
        _buttonController.OnButtonClick += Upgrade;
    }
    
    private void OnDestroy()
    {
        GameManager.Instance.OnMoneyValueChange -= _buttonController.OnMoneyValueChange;
        _buttonController.OnButtonClick -= Upgrade;
    }

    private void UpdateButtonState()
    {
        if (!IsUnlock)
        {
            _buttonController.UpdateButton("BUY", _itemsContainer.UnlockPrice);
        }
        else if (_itemsContainer.IsUpgradeExist(Level))
        {
            _buttonController.UpdateButton("UPGRADE", GetPrice(Level));
        }
        else
        {
            _buttonController.gameObject.SetActive(false);
        }
    }

    private float GetPrice(int level)
    {
        return (float) Math.Round(_itemsContainer.StartUpgradePrice * Mathf.Pow(_itemsContainer.PriceMultiplier, level), 2);
    }

    private void SetModel(int level)
    {
        var buildItemConfig = _itemsContainer.GetUpgrade(level);
        if (_currentModel != null)
        {
            Destroy(_currentModel);
        }

        _currentModel = Instantiate(buildItemConfig.Model);
        _currentModel.transform.parent = _modelPoint;
        _currentModel.transform.localPosition = Vector3.zero;
        _currentModel.transform.localRotation = _modelPoint.rotation;

        if (_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);
        }

        _timerCoroutine = StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            OnProccess?.Invoke(_itemsContainer.GetUpgrade(Level).ProcessResult);
        }
    }

    private void Upgrade()
    {
        if (!IsUnlock)
        {
            IsUnlock = true;
            UpdateButtonState();
            SetModel(Level);
            OnBuildUpgrade?.Invoke(_itemsContainer.UnlockPrice);
        }
        else if(_itemsContainer.IsUpgradeExist(Level + 1))
        {
            Level++;
            UpdateButtonState();
            SetModel(Level);
            OnBuildUpgrade?.Invoke(GetPrice(Level));
        }
    }
}
