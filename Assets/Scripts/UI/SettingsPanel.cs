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

    [SerializeField] private GameObject _touchPanel;
    [SerializeField] private GameObject _lookJoystick;

    [SerializeField] private Toggle _toggleJoystick;
    [SerializeField] private Toggle _toggleTouchpad;

    private float _rotateSpeed { get { return _firstPersonController.RotationSpeed; } set { _firstPersonController.RotationSpeed = value; } }

    private void Awake()
    {
        _sensaSlider.value = PlayerPrefs.GetFloat("rotateSpeed", 2f);
        string kindOfControl = PlayerPrefs.GetString("kindOfControl");

        if (kindOfControl.Equals("joystick"))
        {
            _toggleJoystick.isOn = true;
            ToogleJoystickChange();
        }
        else
        {
            _toggleJoystick.isOn = false;
            ToogleJoystickChange();
        }

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

        if (_toggleJoystick.isOn == true)
            PlayerPrefs.SetString("kindOfControl", "joystick");
        else if (_toggleTouchpad.isOn == true)
            PlayerPrefs.SetString("kindOfControl", "touchpad");

        gameObject.SetActive(false);
    }

    public void ToogleJoystickChange()
    {
        if (_toggleJoystick.isOn == true)
            _toggleTouchpad.isOn = false;
        else
            _toggleTouchpad.isOn = true;

        TurnOnCurrentControls();
    }

    public void ToogleTouchpadChange()
    {
        if (_toggleTouchpad.isOn == true)
            _toggleJoystick.isOn = false;
        else
            _toggleJoystick.isOn = true;

        TurnOnCurrentControls();
    }

    private void TurnOnCurrentControls()
    {
        if (_toggleTouchpad.isOn == true)
        {
            _lookJoystick.SetActive(false);
            _touchPanel.SetActive(true);
        }
        else if (_toggleJoystick.isOn == true)
        {
            _lookJoystick.SetActive(true);
            _touchPanel.SetActive(false);
        }
    }
}
