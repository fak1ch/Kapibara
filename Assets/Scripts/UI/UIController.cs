using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Action OnGameStart;

    [SerializeField] private GameObject _uiGameProcess;
    [SerializeField] private GameObject _uiOtherPanels;
    [SerializeField] private GameObject _uiButtons;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _selectGameModePanel;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    public void PlayButtonClick()
    {
        _selectGameModePanel.SetActive(true);
    }

    public void StartGame()
    {
        _uiGameProcess.SetActive(true);
        _uiButtons.SetActive(false);
        _selectGameModePanel.SetActive(false);
        PlayerEventsController.Instance
            .GetPlayer
            .gameObject
            .SetActive(true);
        OnGameStart?.Invoke();
    }

    public void RestartGame()
    {
        HideGameOverPanel();
        PlayerEventsController.Instance
            .GetPlayer
            .RecreatePlayer();
    }

    public void OpenInfo(GameObject infoPanel)
    {
        infoPanel.SetActive(true);
    }

    public void ShowSettings(GameObject settings)
    {
        settings.SetActive(true);
    }

    public void ShowGameOverPanel()
    {
        _gameOverPanel.SetActive(true);
    }

    public void HideGameOverPanel()
    {
        _gameOverPanel.SetActive(false);
    }
}
