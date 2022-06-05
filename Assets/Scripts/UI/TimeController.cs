using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public Action OnMinuteLater;
    public Action OnLastTenSeconds;
    public Action OnGamePassed;

    [SerializeField] private float _minutesUntilGameOver = 5f;

    private Text _timeText;
    private float _secUntilGameOver;
    private float _secOneMinuteLater;
    private bool _gameInProgress = true;
    private bool _lastTenSeconds = false;

    public float SecUntilGameOver => _secUntilGameOver;
    public float GetSecUntilGameOver { set { _secOneMinuteLater = value; } }

    private void Start()
    {
        _timeText = GetComponent<Text>();
        _secUntilGameOver = _minutesUntilGameOver * 60;
        _secOneMinuteLater = _secUntilGameOver;
        OnMinuteLater?.Invoke();
    }

    private void FixedUpdate()
    {
        if (_gameInProgress == true)
        {
            RefreshTimer();
            _secUntilGameOver -= Time.fixedDeltaTime;
            if (_secUntilGameOver <= _secOneMinuteLater - 60)
            {
                _secOneMinuteLater = _secUntilGameOver;
                OnMinuteLater?.Invoke();
            }

            if (_secUntilGameOver <= 10 && _lastTenSeconds == false)
            {
                _lastTenSeconds = true;
                OnLastTenSeconds?.Invoke();
            }
        }
    }

    private void RefreshTimer()
    {
        int minutes = Mathf.FloorToInt(_secUntilGameOver / 60);
        int seconds = Mathf.FloorToInt(_secUntilGameOver - minutes * 60);

        if (seconds < 10)
            _timeText.text = $"{minutes}:0{seconds}";
        else
        _timeText.text = $"{minutes}:{seconds}";

        if (_secUntilGameOver <= 0)
        {
            _gameInProgress = false;
            OnGamePassed?.Invoke();
            PlayerEventsController.Instance
                .GetPlayer
                .MoveAndRotatePlayer(new Vector3(0, 0, -51), new Vector3(0, 90, 0));
            gameObject.SetActive(false);
        }
    }

    public void TimeContinue()
    {
        OnMinuteLater?.Invoke();
        _gameInProgress = true;
    }

    public void RestartTime()
    {
        _secUntilGameOver = _minutesUntilGameOver * 60;
        _secOneMinuteLater = _secUntilGameOver;
        _gameInProgress = true;
        _lastTenSeconds = false;
        OnMinuteLater?.Invoke();
    }

    public void PauseTime()
    {
        _gameInProgress = false;
    }
}
