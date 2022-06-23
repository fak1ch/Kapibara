using System;
using UnityEngine;

public class PlayerEventsController : MonoSingleton<PlayerEventsController>
{
    [SerializeField] private Player _player;
    [SerializeField] private TimeController _timeController;
    [SerializeField] private UIController _uiController;

    public Player GetPlayer
    {
        get
        {
            return _player;
        }
    }

    private void OnEnable()
    {
        _player.OnGameOver += GameOver;
        _player.OnGameRestart += GameRestart;
        _player.OnGameContinue += GameContinue;
    }

    private void OnDisable()
    {
        _player.OnGameOver -= GameOver;
        _player.OnGameRestart -= GameRestart;
        _player.OnGameContinue -= GameContinue;
    }

    public void GameOver()
    {
        _player.gameObject.SetActive(false);
        _timeController.PauseTime();
        _uiController.ShowGameOverPanel();
    }

    private void GameRestart()
    {
        _timeController.RestartTime();
        _uiController.HideGameOverPanel();
    }

    private void GameContinue()
    {
        _timeController.TimeContinue();
        _uiController.HideGameOverPanel();
    }
}
