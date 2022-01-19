using System;
using DG.Tweening;
using UnityEngine;

public class ButtonAnim : MonoBehaviour
{
    [SerializeField] private GameObject _startScreen;
    [SerializeField] private GameObject _completeScreen;

    [SerializeField] private ButtonType _buttonType = ButtonType.None;
    private Action Complete;
    private Vector2 _hiddenPose;
    private Vector2 _normalPose;

    private void Start()
    {
        _hiddenPose = new Vector2(Screen.width * 0.5f, Screen.height * 1.5f);
        _normalPose = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        Complete += ShowCompleteScreen;
    }

    public async void DoSome()
    {
        await transform.DOScale(1.1f, .3f).AsyncWaitForCompletion();
        transform.DOScale(1f, .3f);
        if (_buttonType == ButtonType.Back)
        {
            _startScreen.transform.position = _hiddenPose;
            await _completeScreen.transform.DOMove(_hiddenPose, 2f).AsyncWaitForCompletion();
            _completeScreen.SetActive(false);
            _startScreen.SetActive(true);
            _startScreen.transform.DOMove(_normalPose, 2f);
        }
        else
        {
            await _startScreen.transform.DOMove(_hiddenPose, 2f).AsyncWaitForCompletion();
            _startScreen.SetActive(false);
            Character.inst.WakeUp(_buttonType, Complete);
        }
    }

    private void ShowCompleteScreen()
    {
        _completeScreen.transform.position = _hiddenPose;
        _completeScreen.SetActive(true);
        _completeScreen.transform.DOMove(_normalPose, 2f);
    }
}

public enum ButtonType
{
    None,
    Back,
    StartUpdate,
    StartAsync,
    StartCoroutine
}
