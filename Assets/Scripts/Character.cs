using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static Character inst;

    [SerializeField] private GameObject _completeScreen;

    private Action OnComplete;
    private void Awake()
    {
        inst = this;
    }

    public void WakeUp(ButtonType type, Action action)
    {
        OnComplete = action;
        switch (type)
        {
            case ButtonType.StartAsync:
                OnComplete.Invoke();
                break;
            case ButtonType.StartCoroutine:
                OnComplete.Invoke();
                break;
            case ButtonType.StartUpdate:
                OnComplete.Invoke();
                break;
        }
    }
}
