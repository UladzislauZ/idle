using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToCam : MonoBehaviour
{
    private void Start()
    {
        Transform cam = Camera.main.transform;
        Vector3 camPos = cam.position;
        Vector3 currentPos = transform.position;

        Vector3 direction = (currentPos - camPos).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
