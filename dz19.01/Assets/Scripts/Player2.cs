using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    [SerializeField] private AnimationController _animation = null;
    [SerializeField] private float _speed = 5f;

    private Vector3 _startPoint = new Vector3(0, 0.6f, 0);
    private Vector3 _finishPos = new Vector3(0, 0.6f, 30);

    private void Start()
    {
        _animation.SetTrigger("Wait");
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        yield return new WaitForSeconds(1f);

        if (transform.rotation.y == 0f)
        {
            while (transform.position != _finishPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, _finishPos, _speed * Time.deltaTime);
                _animation.SetTrigger("Run");
                _animation.SetTrigger("StopWait");
                yield return null;

            }
        }
        while (transform.position == _finishPos)
        {
            _animation.SetTrigger("StopRun");
            _animation.SetTrigger("Wait");
            yield return null;
            yield return new WaitForSeconds(1f);
            _animation.SetTrigger("StopWait");
            _animation.SetTrigger("Jump");
            Rotation();
            yield return new WaitForSeconds(2f);
            transform.position = Vector3.MoveTowards(transform.position, _finishPos, _speed * Time.deltaTime);
            _animation.SetTrigger("Run");
            _animation.SetTrigger("StopWait");
        } 
        
        

    }
    private void Rotation()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(180f, Vector3.up), 1000000 * Time.deltaTime);
        return;
        
    }
}
