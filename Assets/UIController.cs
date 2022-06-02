using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public Action OnGameStart;

    [SerializeField] private GameObject _uiGameProcess;
    [SerializeField] private GameObject _uiOtherPanels;
    [SerializeField] private GameObject _uiButtons;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Player _player;

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

    private void Start()
    {

    }

    public void StartGame()
    {
        _uiGameProcess.SetActive(true);
        _uiButtons.SetActive(false);
        _player.gameObject.SetActive(true);
        OnGameStart?.Invoke();
    }

    public void RestartGame()
    {
        _player.transform.position = new Vector3(0, 0, 0);
        _player.transform.rotation = Quaternion.Euler(0, 90, 0);
        HideGameOverPanel();
        _player.GameRestart();
    }

    public void OpenInfo(GameObject infoPanel)
    {
        infoPanel.SetActive(true);
    }

    public void ShowSettings(GameObject settings)
    {
        settings.SetActive(true);
    }

    private void ShowGameOverPanel()
    {
        _gameOverPanel.SetActive(true);
    }

    public void HideGameOverPanel()
    {
        _gameOverPanel.SetActive(false);
    }
}
