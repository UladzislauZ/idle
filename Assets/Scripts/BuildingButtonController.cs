using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButtonController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _titelText;

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
        _titelText.text = title;
        _priceText.text = price.ToString();
        _priceValue = price;
    }

    public void OnMoneyValueChange(float money)
    {
        SetState(money >= _priceValue);
    }
    
}