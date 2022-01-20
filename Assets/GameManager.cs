using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    private UIController _uIController;
    
    private void Awake()
    {
        _uIController = FindObjectOfType<UIController>();

    }
   
    public void StartGame()
    {
        player.gameObject.SetActive(true);
        _uIController.OpenGame();
        player.ActivatePlayer();
        SubscribePlayer();
    }
    public void StartGameWithCoroutine()
    {
        player.gameObject.SetActive(true);
        _uIController.OpenGame();
        player.ActivatePlayerCoroutine();
        SubscribePlayer();
    }
    public void StartGameWithUnitask()
    {
        player.gameObject.SetActive(true);
        _uIController.OpenGame();
        player.ActivatePlayerUnitask();
        SubscribePlayer();
    }
    public void FinishGame()
    {
        player.gameObject.SetActive(false);
        _uIController.OpenComplete();
        UnSubscribePlayer();
    }
    private void SubscribePlayer()
    {
        player.OnFinish += FinishGame;
    }
    private void UnSubscribePlayer()
    {
        player.OnFinish -= FinishGame;
       
    }
    public void RestartScene()
    {
        _uIController.OpenMenu();
    }

}
