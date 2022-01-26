using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButtonController : MonoBehaviour
{
    public event Action OnButtonClick;
    
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _titleText;

    private Button _button;
    private float _priceValue;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void Initialize()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        OnButtonClick?.Invoke();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void SetState(bool isActive)
    {
        _button.interactable = isActive;
    }

    public void UpdateButton(string title, float price)
    {
        _titleText.text = title;
        _priceText.text = price.ToString();
        _priceValue = price;
    }

    public void OnMoneyChanged(float money)
    {
        SetState(money >= _priceValue);
    }
}
