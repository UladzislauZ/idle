using UnityEngine;

public class UiController : MonoBehaviour
{
   [SerializeField] private GameObject startPanel;
   [SerializeField] private GameObject gamePane;

   public void SwitchPanels(bool isMenu)
   {
      startPanel.SetActive(isMenu);
      gamePane.SetActive(!isMenu);
   }
}
