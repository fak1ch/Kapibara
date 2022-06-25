using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranslatableText : MonoBehaviour
{
    [SerializeField] private int _id;

    private Text _text;

    public Text GetText => _text;
    public int GetId => _id;

    public bool IsFirstChange = true;

    private void Start()
    {
        _text = GetComponent<Text>();

        Translator.Add(this);
        IsFirstChange = false;
    }
}
