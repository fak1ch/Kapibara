using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizePanel : MonoBehaviour
{
    [SerializeField] private GameObject _capybariesPanel;
    [SerializeField] private GameObject _landscapePanel;

    [SerializeField] private Image _capybariesButtonBg;
    [SerializeField] private Image _landscapeButtonBg;
    [SerializeField] private Text _capybariesText;
    [SerializeField] private Text _landscapeText;

    private float _startMaxBgOpacity;
    private float _startMaxTextOpacity;

    private void Start()
    {
        _startMaxBgOpacity = _capybariesButtonBg.color.a;
        _startMaxTextOpacity = _capybariesText.color.a;

        ShowCapybariesPanel();
    }

    public void ShowCapybariesPanel()
    {
        _capybariesPanel.SetActive(true);
        _landscapePanel.SetActive(false);

        ChangePage(_landscapeButtonBg, _landscapeText, _capybariesButtonBg, _capybariesText);
    }

    public void ShowLandscapePanel()
    {
        _capybariesPanel.SetActive(false);
        _landscapePanel.SetActive(true);

        ChangePage(_capybariesButtonBg, _capybariesText, _landscapeButtonBg, _landscapeText);
    }

    public void HideShopPanel()
    {
        gameObject.SetActive(false);
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
