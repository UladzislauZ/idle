using UnityEngine;
using UnityEngine.UI;

public class LookAtCamera : MonoBehaviour
{
    private Button _button;
    private void Awake()
    {
        _button = GetComponentInChildren<Button>();
       // _button.onClick.AddListener(OnClick);
        Transform camera = Camera.main.transform;
        Vector3 cameraPos = camera.position;
        Vector3 currentPos = transform.position;

        Vector3 direction = (currentPos - cameraPos).normalized; 
        transform.rotation = Quaternion.LookRotation(direction);
    }


    public void OnClick()
    {
        Debug.Log("Was click");
    }
}
