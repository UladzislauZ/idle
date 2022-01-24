using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour
{
    public static Character inst;

    [SerializeField] private TestSaveManager _saveManager;
    private CharacterData _characterData;
    private Action OnComplete;
    private bool _isAlive;
    private int _state = 0;
    private int _activeState = 0;
    
    private void Awake()
    {
        inst = this;
        _saveManager.Initialize();
        _characterData = _saveManager.CharacterData;
    }

    public void WakeUp(ButtonType type, Action action)
    {
        OnComplete = action;
        
        transform.position = _characterData.position;
        _state = _characterData.State;
        
        switch (type)
        {
            case ButtonType.StartAsync:
                MoveAsyncDOTwen();
                break;
            case ButtonType.StartCoroutine:
                StartCoroutine(MoveCoroutine());
                break;
            case ButtonType.StartUpdate:
                
                _isAlive = true;
                break;
        }
    }

    private void OnApplicationQuit()
    {
        _saveManager.CharacterData.State = _activeState;
        _saveManager.CharacterData.position = transform.position;
        
        _saveManager.SaveData();
    }

    private void Update()
    {
        if(!_isAlive)
            return;

        if (_state == 0)
        {
            _state = -1;
            transform.DOMove(Vector3.forward * 3f, 3f)
                .SetEase(Ease.Flash)
                .OnComplete(() =>
            {
                _state = 1;
                _activeState = 1;
            });
        }

        if (_state == 1)
        {
            _state = -1;
            transform.DORotate(new Vector3(0, 180, 0), 1.9f);
            transform.DOJump(transform.position, 2f, 1, 2f)
                .OnComplete(() =>
            {
                _state = 2;
                _activeState = 2;
            });
        }

        if (_state == 2)
        {
            _state = -1;
            transform.DOMove(Vector3.zero, 3f)
                .OnComplete(() =>
            {
                _state = 3;
                _activeState = 3;
            });
        }

        if (_state == 3)
        {
            _state = 0;
            _activeState = 0;
            transform.rotation = Quaternion.Euler(Vector3.zero);
            OnComplete.Invoke();
            _isAlive = false;
        }

    }
    

    private IEnumerator MoveCoroutine()
    {
        if (_state == 0)
        {
            yield return new DOTweenCYInstruction.WaitForCompletion(transform.DOMove(Vector3.forward * 3f, 3f));
            _state = 1;
            _activeState = 1;
        }
        if (_state == 1)
        {
            transform.DORotate(new Vector3(0, 180, 0), 1.9f);
            yield return new DOTweenCYInstruction.WaitForCompletion(transform.DOJump(transform.position, 2f, 1, 2f));
            _state = 2;
            _activeState = 2;
        }
        if (_state == 2)
        {
            yield return new DOTweenCYInstruction.WaitForCompletion(transform.DOMove(Vector3.zero, 2f));
            _state = 3;
            _activeState = 3;
        }
            
            
        /* //Based on transform.position
        int state = 0;
         
        while (state == 0)
        {
            transform.Translate(Vector3.forward * 2f * Time.deltaTime);
            yield return null;
            if (transform.position.z > 3)
                state = 1;
        }
        while (state == 1)
        {
            transform.Translate(Vector3.up * 2f * Time.deltaTime);
            yield return null;
            if (transform.position.y > 2)
                state = 2;
            
            if(transform.rotation.eulerAngles.y < 180)
                transform.Rotate(0, 2, 0);
        }
        while (state == 2)
        {
            transform.Translate(Vector3.down * 2f * Time.deltaTime);
            yield return null;
            if (transform.position.y <= 0)
                state = 3;
            
            if(transform.rotation.eulerAngles.y < 180)
                transform.Rotate(0, 2, 0);
        }
        while (state == 3)
        {
            transform.Translate(Vector3.forward * 2f * Time.deltaTime);
            yield return null;
            if (transform.position.z <= 0)
                state = 4;
        }
        
        transform.position = Vector3.zero;
        */
        
        if (_state == 3)
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
            OnComplete.Invoke();
            _state = 0;
            _activeState = 0;
        }
    }

    private async void MoveAsyncDOTwen()
    {
        if (_state == 0)
        {
            await transform.DOMove(Vector3.forward * 3f, 3f).AsyncWaitForCompletion();
            _state = 1;
            _activeState = 1;
        }

        if (_state == 1)
        {
            transform.DORotate(new Vector3(0, 180, 0), 1.9f);
            await transform.DOJump(transform.position, 2f, 1, 2f).AsyncWaitForCompletion();
            _state = 2;
            _activeState = 2;
        }

        if (_state == 2)
        {
            await transform.DOMove(Vector3.zero, 3f).AsyncWaitForCompletion();
            _state = 3;
            _activeState = 3;
        }

        if (_state == 3)
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
            OnComplete.Invoke();
            _state = 0;
            _activeState = 0;
        }
    }
}
