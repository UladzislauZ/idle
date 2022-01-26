using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Start()
    {
        OnMoneyChanged(GameManager.Instance.Money);
        GameManager.Instance.OnMoneyValueChange += OnMoneyChanged;
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnMoneyValueChange -= OnMoneyChanged;
    }
    private void OnMoneyChanged(float value)
    {
        moneyText.text = value.ToString();
        Debug.Log(value);
    }
    
}
