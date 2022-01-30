using System.Collections;
using System.Collections.Generic;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseInit : MonoBehaviour
{
    private void Awake()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(
          task =>
          {
              if (task.Exception != null)
              {
                  Debug.LogException(task.Exception);
                  return;
              }
              Debug.Log("Fire init");
          });
    }
}
