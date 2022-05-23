using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Action OnGameOver;
    public Action OnGameRestart;
    public Action OnGameContinue;

    [SerializeField] private float _health = 5f;
    [SerializeField] private RewardedAdsButton _rewardedAdsButton;
    private Vector3 _startPosition;
    private bool _isImmortalityActive = false;

    private void Start()
    {
        _startPosition = transform.position;
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
