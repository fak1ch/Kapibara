using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private float _timeUntilHide = 5f;

    [SerializeField] private Text _vk;
    [SerializeField] private Text _tt;
    [SerializeField] private Text _gmail;

    private void OnEnable()
    {
        StartCoroutine(HideInfoPanelAfterTime());
    }

    private IEnumerator HideInfoPanelAfterTime()
    {
        yield return new WaitForSeconds(_timeUntilHide);
        gameObject.SetActive(false);
    }

    public void OpenUrlVK()
    {
        Application.OpenURL($"https://www.{_vk.text}");
    }

    public void OpenUrlTT()
    {
        Application.OpenURL($"https://www.{_tt.text}");
    }

    public void OpenUrlGmail()
    {
        Application.OpenURL($"https://www.{_gmail.text}");
    }
}
