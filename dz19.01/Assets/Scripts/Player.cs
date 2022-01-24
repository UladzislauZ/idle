using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private AnimationController _animation = null;
    [SerializeField] private float _speed = 0f;
    [SerializeField] private GameObject _startScreen;
    [SerializeField] private GameObject _plane;

    private Vector3 _startPoint = new Vector3(0,0.6f,0);
    private Vector3 _finishPos = new Vector3(0, 0.6f, 30);

    public float _time1 = 5;
    public float _time2 = 5;
    public float _time3 = 5;
    public float _time4 = 5;

    public Move _move;

    public enum Move
    {
        HideUI, WaitForStartMove, MoveToPoint, WaitForJump, JumpAndRotate, WaitForMoveBack, MoveBack, ShowUI
    }

    private void StartGame()
    {
        _plane.SetActive(true);
    }

    public void OnClick()
    {
        StartGame();
    }

    private void Update()
    {
        switch(_move)
        {
            case Move.HideUI:

                _startScreen.SetActive(false);
                break;

            case Move.WaitForStartMove:

                _animation.SetTrigger("Wait");
                break;

            case Move.MoveToPoint:

                transform.position = Vector3.MoveTowards(transform.position, _finishPos, _speed * Time.deltaTime);
                _animation.SetTrigger("Run");
                break;

            case Move.WaitForJump:

                _animation.SetTrigger("StopRun");
                _animation.SetTrigger("Wait");
                break;

            case Move.JumpAndRotate:
                
                    _animation.SetTrigger("StopWait");
                    _animation.SetTrigger("Jump");
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(180f, Vector3.up), 50 * Time.deltaTime); 
                
                break;
            case Move.WaitForMoveBack:
                
                _animation.SetTrigger("StopJump");
                _animation.SetTrigger("Wait");
                break;

            case Move.MoveBack:
                
                transform.position = Vector3.MoveTowards(transform.position, _startPoint, _speed * Time.deltaTime);
                _animation.SetTrigger("Run");
                break;

            case Move.ShowUI:

                _startScreen.SetActive(true);
                break;
        }
        if(transform.position == _startPoint)
        {
            _move = Move.HideUI;
            _move = Move.WaitForStartMove;
            _time1 -= Time.deltaTime;
        }

        if (_time1 <= 0)
        { 
            if (transform.position != _finishPos)
            {
                _move = Move.MoveToPoint;
            }
        }

        if (transform.position == _finishPos && transform.rotation.y > -180f)
        {
            _move = Move.WaitForJump;
            _time2 -= Time.deltaTime;

            if (_time2 <= 0)
            {
                _move = Move.JumpAndRotate;
                
            }
            
        } 

            if (transform.rotation.y <= -180f && transform.position == _finishPos)
            {
                Debug.Log("ok");
                _time3 -= Time.deltaTime;
                if (_time3 <= 0)
                {
                    _move = Move.WaitForMoveBack;
                    Debug.Log("все ок");
                }
            }
        
        

      /*  if (transform.position != _startPoint)
        {
            _move = Move.MoveBack;
        }*/
    }

    
}
