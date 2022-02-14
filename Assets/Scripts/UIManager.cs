using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _startScreen = null;
    [SerializeField] private GameObject _levelScreen = null;
    [SerializeField] private TextMeshProUGUI scoreLabel = null;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _saveButton;

    public event Action OnLoadLevel;
    public event Action OnSave;

    public void LoadGame()
    {
        _startScreen.SetActive(true);
        _startButton.onClick.AddListener(LoadLevel);
    }

    public void LoadLevel()
    {
        _levelScreen.SetActive(true);
        _startScreen.SetActive(false);
        _saveButton.onClick.AddListener(Save);
        OnLoadLevel?.Invoke();
    }

    public void PrintScore(string score)
    {
        scoreLabel.text = score;
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(LoadLevel);
        _saveButton.onClick.RemoveListener(Save);
    }

    public void Save()
    {
        OnSave?.Invoke();
    }
}
