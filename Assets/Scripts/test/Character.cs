using System;
using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine.Jobs;

public class Character : MonoBehaviour
{
    public static Character inst;

    private Action OnComplete;
    private bool _isAlive;
    private int _state = 0;
    
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

    private void Update()
    {
        if(!_isAlive)
            return;

        if (_state == 0)
        {
            _state = -1;
            transform.DOMove(Vector3.forward * 3f, 3f).SetEase(Ease.Flash).OnComplete(() => _state = 1);
        }

        if (_state == 1)
        {
            _state = -1;
            transform.DORotate(new Vector3(0, 180, 0), 1.9f);
            transform.DOJump(transform.position, 2f, 1, 2f).OnComplete(() => _state = 2);
        }

        if (_state == 2)
        {
            _state = -1;
            transform.DOMove(Vector3.zero, 3f).OnComplete(() => _state = 3);
        }

        if (_state == 3)
        {
            _state = 0;
            transform.rotation = Quaternion.Euler(Vector3.zero);
            OnComplete.Invoke();
            _isAlive = false;
        }

    }
    

    private IEnumerator MoveCoroutine()
    {
        yield return new DOTweenCYInstruction.WaitForCompletion(transform.DOMove(Vector3.forward * 3f, 3f));
        transform.DORotate(new Vector3(0, 180, 0), 1.9f);
        yield return new DOTweenCYInstruction.WaitForCompletion(transform.DOJump(transform.position, 2f, 1, 2f));
        yield return new DOTweenCYInstruction.WaitForCompletion(transform.DOMove(Vector3.zero, 2f));
        
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
        
        transform.rotation = Quaternion.Euler(Vector3.zero);
        OnComplete.Invoke();
    }

    private async void MoveAsyncDOTwen()
    {
        await transform.DOMove(Vector3.forward * 3f, 3f).AsyncWaitForCompletion();
        transform.DORotate(new Vector3(0, 180, 0), 1.9f);
        await transform.DOJump(transform.position, 2f, 1, 2f).AsyncWaitForCompletion();
        await transform.DOMove(Vector3.zero, 3f).AsyncWaitForCompletion();
        transform.rotation = Quaternion.Euler(Vector3.zero);
        OnComplete.Invoke();
    }
}
