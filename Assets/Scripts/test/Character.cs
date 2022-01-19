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
    private int _animState = 0;
    
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
                break;
            case ButtonType.StartCoroutine:
                StartCoroutine(PlayAnim());
                break;
            case ButtonType.StartUpdate:
                _isAlive = true;
                break;
        }
    }

    private IEnumerator PlayAnim()
    {
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
        transform.rotation = Quaternion.Euler(Vector3.zero);
        OnComplete.Invoke();
    }
}
