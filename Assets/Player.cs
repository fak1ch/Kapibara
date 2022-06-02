using System;
using System.Collections;
using System.Collections.Generic;
using TouchNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Action OnGameOver;
    public Action OnGameRestart;
    public Action OnGameContinue;

    [SerializeField] private float _health = 5f;
    [SerializeField] private RewardedAdsButton _rewardedAdsButton;

    private TouchControl _touchControl;
    private Vector3 _startPosition;
    private bool _isImmortalityActive = false;

    private void Awake()
    {
        _touchControl = new TouchControl();
    }

    private void OnEnable()
    {
        _touchControl.Enable();
    }

    private void OnDisable()
    {
        _touchControl.Disable();
    }

    private void Start()
    {
        _startPosition = transform.position;
        _touchControl.Player.TouchPress.started += ctx => StartTouch(ctx);
        _touchControl.Player.TouchPress.canceled += ctx => EndTouch(ctx);
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch started " + _touchControl.Player.TouchPosition.ReadValue<Vector2>());
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch end " + _touchControl.Player.TouchPosition.ReadValue<Vector2>());
    }

    public void TakeDamage(int damage)
    {
        if (_isImmortalityActive == false)
        {
            if (damage <= 0)
            {
                Debug.Log("Damage must be > 0");
                return;
            }

            if (_health > 0)
                _health -= damage;

            if (_health <= 0)
            {
                _health = 0;
                OnGameOver?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }

    public void GameRestart()
    {
        _rewardedAdsButton.ShowAdButton.enabled = true;
        gameObject.SetActive(true);
        _health = 1;
        OnGameRestart?.Invoke();
    }

    public void GameContinue()
    {
        _rewardedAdsButton.ShowAdButton.enabled = false;
        _health = 1;
        FindObjectOfType<UIController>().HideGameOverPanel();
        OnGameContinue?.Invoke();
    }
}
