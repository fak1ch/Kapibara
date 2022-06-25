using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    private bool _isOn = true;

    private void OnTriggerEnter(Collider other)
    {
        if (_isOn == true)
        {
            if (other.TryGetComponent(out Player player))
            {
                _isOn = false;
                SceneManager.LoadSceneAsync("Playground", LoadSceneMode.Single);
            }
        }
    }
}
