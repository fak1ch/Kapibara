using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private TimeController _timeController;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _timeController.OnGamePassed += PlayRelaxMusic;
    }

    private void OnDisable()
    {
        _timeController.OnGamePassed -= PlayRelaxMusic;
    }

    private void PlayRelaxMusic()
    {
        _audioSource.Play();
    }
}
