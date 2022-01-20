using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _gamePanel;

    public void SwitchPanel(bool isMenu)
    {
        _startPanel.SetActive(isMenu);
        _gamePanel.SetActive(!isMenu);
        
    }
}
