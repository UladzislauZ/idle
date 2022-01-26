using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;

    private void Start()
    {
        OnMoneyChanged(GameManager.Inst.Money);
        GameManager.Inst.OnMoneyValueChange += OnMoneyChanged;
    }

    private void OnDestroy()
    {
        GameManager.Inst.OnMoneyValueChange -= OnMoneyChanged;
    }

    private void OnMoneyChanged(float value)
    {
        _moneyText.text = value.ToString();
    }
}
