using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action OnFinish;

    private Rigidbody _rb;
    private Vector3 _jump;

    private float _jumpForce = 2f;
    private float _distanceZ = 13f;
    private float _timer = 0;
    private float _timeToWait = 1f;
    private int _state;
    private bool _isUpdateMethod;
    private float _playerSpeed = 1f;

    private void Start()
    {
        _rb = GetComponentInChildren<Rigidbody>();
        _jump = new Vector3(0.0f, 3.0f, 0.0f);
    }
    private void Update()
    {
        if (_isUpdateMethod)
        {
            PlayerAnimationUpdate();
        }
    }

    public void ActivatePlayer()
    {
        gameObject.SetActive(true);
       
        _isUpdateMethod = true;
        _state = 1;
        
    }
    public void ActivatePlayerCoroutine()
    {
        gameObject.SetActive(true);
        _state = 1;
        StartCoroutine(PlayerAnimationCoroutine());
       
    }
    public void ActivatePlayerUnitask()
    {
        gameObject.SetActive(true);
        _ = PlayerAnimationUnitask();
    }
    private async UniTask PlayerAnimationUnitask()
    {
        var ct = this.GetCancellationTokenOnDestroy();
        await UniTask.WhenAll(transform.DOMoveZ(15f, 3f).WithCancellation(ct));  
        await UniTask.WhenAll(transform.DOJump(transform.position, 1f, 1, 1f).WithCancellation(ct));
        await UniTask.WhenAll(transform.DORotate(new Vector3(0, 180, 0), 1f).WithCancellation(ct));
        await UniTask.WhenAll(transform.DOMoveZ(10f, 3f).WithCancellation(ct));
        Finish();
    }
    private IEnumerator PlayerAnimationCoroutine()
    {
        yield return new WaitForSeconds(1);
        while (_state == 1)
        {
            PlayerMoveForward();
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        while (_state == 2)
        {
           PlayerJump();
           yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        while (_state == 3)
        {
            PlayerRotate();
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        while (_state == 4)
        {
            PlayerMoveBack();
            yield return null;
        }

    }
   
    private void PlayerAnimationUpdate()
    {
        _timer += Time.deltaTime;
        if (_timer > _timeToWait)
        {
            if (_state == 1)
            {
                PlayerMoveForward();
                _timeToWait = 0.5f;
            }
            else if (_state == 2)
            {
                PlayerJump();
                _timeToWait = 0f;
            }
            else if (_state == 3)
            {
                PlayerRotate();
                _timeToWait = 0.5f;
            }
            else if (_state == 4)
            {
                PlayerMoveBack();
                _timeToWait = 1f;
            }
        }
    }
    private void PlayerMoveForward()
    {
        float step = _playerSpeed * Time.deltaTime;
        transform.Translate(Vector3.forward * step);
        if (transform.position.z > _distanceZ)
        {
            _state = 2;
            _timer = 0;
        }
    }
    private void PlayerJump()
    {
        _rb.AddForce(_jump * _jumpForce, ForceMode.Impulse);
        _state = 3;
        _timer = 0;
    }
    private void PlayerRotate()
    {
        if (transform.rotation.eulerAngles.y < 180)
        {
            transform.Rotate(0, 1, 0);

        }
        else
        {
            _state = 4;
            _timer = 0;
        }
    }
    private void PlayerMoveBack()
    {
        float step = _playerSpeed * Time.deltaTime;
        transform.Translate(Vector3.forward * step);
        if (transform.position.z < 10f)
        {
            _timer = 0;
            Finish();
        }
    }
    private void Finish()
    {
        transform.rotation = Quaternion.identity;
        OnFinish?.Invoke();
    }

}
