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
    [SerializeField] private Player _player;

    private Text _timeText;
    private float _secUntilGameOver;
    private float _secOneMinuteLater;
    private bool _gameInProgress = true;
    private bool _lastTenSeconds = false;

    public float SecUntilGameOver => _secUntilGameOver;
    public float GetSecUntilGameOver { set { _secOneMinuteLater = value; } }

    private void OnEnable()
    {
        _player.OnGameOver += PauseTime;
        _player.OnGameRestart += RestartTime;
        _player.OnGameContinue += GameContinue;
    }

    private void OnDisable()
    {
        _player.OnGameOver -= PauseTime;
        _player.OnGameRestart -= RestartTime;
        _player.OnGameContinue -= GameContinue;
    }

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

    private void GameContinue()
    {
        OnMinuteLater?.Invoke();
        _gameInProgress = true;
    }

    private void RefreshTimer()
    {
        int minutes = Mathf.FloorToInt(_secUntilGameOver / 60);
        int seconds = Mathf.FloorToInt(_secUntilGameOver - minutes * 60);
        _timeText.text = $"{minutes}:{seconds}";

        if (_secUntilGameOver <= 0)
        {
            _gameInProgress = false;
            OnGamePassed?.Invoke();
            _player.transform.position = new Vector3(0, 0, -51);
            _player.transform.rotation = Quaternion.Euler(0, 90, 0);
            GameObject.FindGameObjectWithTag("RelaxMusic").GetComponent<AudioSource>().Play();
            gameObject.SetActive(false);
        }
    }

    private void RestartTime()
    {
        _secUntilGameOver = _minutesUntilGameOver * 60;
        _secOneMinuteLater = _secUntilGameOver;
        _gameInProgress = true;
        _lastTenSeconds = false;
        OnMinuteLater?.Invoke();
    }

    private void PauseTime()
    {
        _gameInProgress = false;
    }
}
