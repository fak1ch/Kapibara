using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnButton : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _showAdsButton;

    public void Respawn()
    {
        _player.gameObject.SetActive(true);
        _player.RespawnPlayerHere();
    }

    public void ShowRespawnButton()
    {
        gameObject.SetActive(true);
        _showAdsButton.SetActive(false);
        _player.RespawnPlayerButton = GetComponent<Button>();
    }
}
