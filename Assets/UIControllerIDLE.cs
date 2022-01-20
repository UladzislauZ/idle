using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControllerIDLE : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;

    public void SwitchPanels(bool isMenu)
    {
        menuPanel.SetActive(isMenu);
        gamePanel.SetActive(!isMenu);
    }
}
