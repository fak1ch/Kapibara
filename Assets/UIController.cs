using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public Action OnGameStart;

    [SerializeField] private GameObject _ui;
    [SerializeField] private GameObject _info;
    [SerializeField] private GameObject _uiButtons;
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _gameOverPanel;

    private void OnEnable()
    {
        _player.OnGameOver += ShowGameOverPanel;
    }

    private void OnDisable()
    {
        _player.OnGameOver -= ShowGameOverPanel;
    }

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    public void StartGame()
    {
        _ui.SetActive(true);
        _uiButtons.SetActive(false);
        _info.SetActive(false);
        _player.gameObject.SetActive(true);
        OnGameStart?.Invoke();
    }

    private void ShowGameOverPanel()
    {
        _gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        _player.transform.position = new Vector3(0, 0, 0);
        _player.transform.rotation = Quaternion.Euler(0, 90, 0);
        HideGameOverPanel();
        _player.GameRestart();
    }

    public void HideGameOverPanel()
    {
        _gameOverPanel.SetActive(false);
    }

    public void OpenInfo()
    {
        _info.SetActive(true);
    }
}
