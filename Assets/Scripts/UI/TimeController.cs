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
    public Action OnTimeOver;

    [SerializeField] private float _minutesUntilTimeOut = 5f;
    [SerializeField] private bool _defaultGameMode = true;
    [SerializeField] private float _secondsUntilSpawnFirstCapybara;

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
        _secUntilGameOver = _minutesUntilTimeOut * 60;
        _secOneMinuteLater = _secUntilGameOver;

        if (_defaultGameMode)
            OnMinuteLater?.Invoke();
        else
            StartCoroutine(SpawnFirstCapybara());
    }

    private IEnumerator SpawnFirstCapybara()
    {
        yield return new WaitForSeconds(_secondsUntilSpawnFirstCapybara);
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

                if (_defaultGameMode == true)
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
            OnTimeOver?.Invoke();
            TimeOver();
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
        _secUntilGameOver = _minutesUntilTimeOut * 60;
        _secOneMinuteLater = _secUntilGameOver;
        _gameInProgress = true;
        _lastTenSeconds = false;

        gameObject.SetActive(true);

        if (_defaultGameMode)
            OnMinuteLater?.Invoke();
        else
            StartCoroutine(SpawnFirstCapybara());
    }

    public void PauseTime()
    {
        _gameInProgress = false;
    }

    private void TimeOver()
    {
        if (_defaultGameMode == true)
        {
            PlayerEventsController.Instance
                .GetPlayer
                .MoveAndRotatePlayer(new Vector3(0, 0, -51), new Vector3(0, 90, 0));
        }
        else
        {
            PlayerEventsController.Instance.GameOver();
        }
    }
}
