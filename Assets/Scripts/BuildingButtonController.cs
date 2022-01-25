using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class BuildingButtonController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _titleText;

    private Button _button;
    private float _priceValue;

    public event Action OnButtonClick;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }
    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        OnButtonClick?.Invoke();
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

    public void OnMoneyValueChange(float money)
    {
        SetState(money >= _priceValue);
    }
}
