using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePassed : MonoBehaviour
{
    private bool _isOpen = true;

    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isOpen)
        {
            if (other.TryGetComponent(out Player player))
            {
                _isOpen = false;
                PlayerComeToMazeEnd();
            }
        }
    }

    private void PlayerComeToMazeEnd()
    {
        var player = FindObjectOfType<Player>();
        player.YouPassedMaze.SetActive(true);
        player.gameObject.SetActive(false);
        FindObjectOfType<TimeController>().PauseTime();
        FindObjectOfType<CapybaraSpawner>().TimeOver();
    }
}
