using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource _audioSource;
    private BoxCollider _boxCollider2D;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _boxCollider2D = GetComponent<BoxCollider>();
    }

    private void PlayRelaxMusic()
    {
        _audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            PlayRelaxMusic();
            _boxCollider2D.enabled = false;
        }
    }
}
