using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private Slider _sensaSlider;
    [SerializeField] private Text _sensaText;
    [SerializeField] private FirstPersonController _firstPersonController;

    private float _rotateSpeed { get { return _firstPersonController.RotationSpeed; } set { _firstPersonController.RotationSpeed = value; } }

    private void Awake()
    {
        _sensaSlider.value = PlayerPrefs.GetFloat("rotateSpeed", 2f);
        ChangeSensaSlider();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            HideSettings();
    }

    public void ChangeSensaSlider()
    {
        _rotateSpeed = _sensaSlider.value;
        _sensaText.text = Math.Round(_rotateSpeed, 2).ToString();
    }

    public void HideSettings()
    {
        PlayerPrefs.SetFloat("rotateSpeed", _rotateSpeed);
        gameObject.SetActive(false);
    }
}
