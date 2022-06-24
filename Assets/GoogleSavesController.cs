using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleSavesController : MonoBehaviour
{
    [SerializeField] private GameObject _advertisement;
    [SerializeField] private GameObject _removeAdsButton;
    [SerializeField] private RespawnButton _respawnButton;

    private void OnEnable()
    {
        GoogleServices.Instance.OnAdsDeactivateChanged += AdsDeactivateChanged;
        GoogleServices.Instance.OnAuthenticationSuccess += AuthenticationSuccess;
    }

    private void OnDisable()
    {
        GoogleServices.Instance.OnAdsDeactivateChanged -= AdsDeactivateChanged;
        GoogleServices.Instance.OnAuthenticationSuccess -= AuthenticationSuccess;
    }

    private void AdsDeactivateChanged(bool value)
    {
        if (value == true)
        {
            _advertisement.SetActive(false);
            _removeAdsButton.SetActive(false);
            _respawnButton.ShowRespawnButton();
        }
        else
        {
             
        }
    }

    private void AuthenticationSuccess()
    {
        GoogleServices.Instance.LoadData();
    }
}
