using StarterAssets;
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
    [SerializeField] private Camera _mainCamera;

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

    public void RecreatePlayer()
    {
        _rewardedAdsButton.ShowAdButton.enabled = true;
        gameObject.SetActive(true);
        MoveAndRotatePlayer(new Vector3(0, 0, 0), new Vector3(0, 90, 0));
        _health = 1;
        OnGameRestart?.Invoke();
    }

    public void RespawnPlayerHere()
    {
        _rewardedAdsButton.ShowAdButton.enabled = false;
        _health = 1;
        OnGameContinue?.Invoke();
    }

    public void MoveAndRotatePlayer(Vector3 newPosition, Vector3 eulers)
    {
        _mainCamera.transform.position = newPosition;
        _mainCamera.transform.rotation = Quaternion.Euler(eulers);
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(eulers);
    }
}
