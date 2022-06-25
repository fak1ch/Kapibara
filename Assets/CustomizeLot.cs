using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeLot : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] public Sprite _sprite;
    [SerializeField] public Material _groundMaterial;
    [SerializeField] public Material _wallMaterial;
    [SerializeField] private Toggle _toggle;

    public int Id => _id;
    public Toggle Toggle => _toggle;

    private string _key;

    private void Start()
    {
        _toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(_toggle);
        });
    }

    private void ToggleValueChanged(Toggle toggle)
    {
        if (toggle.isOn == true)
        {
            PlayerPrefs.SetInt(_key, 1);
        }
        else
        {
            PlayerPrefs.SetInt(_key, 0);
        }
    }

    public void CheckSelectFlag()
    {
        _key = $"ProductSelect:{_id}";

        if (PlayerPrefs.HasKey(_key))
        {
            int value = PlayerPrefs.GetInt(_key);
            if (value == 1)
                _toggle.isOn = true;
            else
                _toggle.isOn = false;
        }
        else
        {
            PlayerPrefs.SetInt(_key, 0);
            _toggle.isOn = false;
        }
    }
}
