using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator = null;

    

    public void SetTrigger(string name)
    {
        _animator.SetTrigger(name);
        
    }
   
}
