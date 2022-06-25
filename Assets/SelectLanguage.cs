using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLanguage : MonoBehaviour
{
    [SerializeField] private Image _ruButtonBg;
    [SerializeField] private Image _enButtonBg;
    [SerializeField] private Text _ruText;
    [SerializeField] private Text _enText;

    private float _startMaxBgOpacity;
    private float _startMaxTextOpacity;

    [SerializeField] private bool _isMainScene = true;

    private void Start()
    {
        if (_isMainScene)
        {
            _startMaxBgOpacity = _ruButtonBg.color.a;
            _startMaxTextOpacity = _ruText.color.a;
        }

        Translator.Clear();

        if (PlayerPrefs.HasKey("Language") == false)
        {
            if (Application.systemLanguage == SystemLanguage.Russian)
                PlayerPrefs.SetInt("Language", 1);
            else
                PlayerPrefs.SetInt("Language", 0);
        }

        Translator.SelectLanguage(PlayerPrefs.GetInt("Language"));

        ChangeLanguage();
    }

    public void LanguageChange(int languageId)
    {
        PlayerPrefs.SetInt("Language", languageId);
        Translator.SelectLanguage(PlayerPrefs.GetInt("Language"));

        ChangeLanguage();
    }

    public void ChangeLanguage()
    {
        if (_isMainScene)
        {
            if (PlayerPrefs.GetInt("Language") == 1)
                ChangePage(_enButtonBg, _enText, _ruButtonBg, _ruText);
            else
                ChangePage(_ruButtonBg, _ruText, _enButtonBg, _enText);
        }
    }

    private void ChangePage(Image currentImage, Text currentText, Image pastImage, Text pastText)
    {
        Color color = currentImage.color;
        color.a = _startMaxBgOpacity;
        currentImage.color = color;

        color = currentText.color;
        color.a = _startMaxTextOpacity;
        currentText.color = color;

        color = pastImage.color;
        color.a = _startMaxBgOpacity / 2;
        pastImage.color = color;

        color = pastText.color;
        color.a = _startMaxTextOpacity / 2;
        pastText.color = color;
    }
}
