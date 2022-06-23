using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class SelectGameMode : MonoBehaviour
{
    [SerializeField] private GameObject _easyMazeTextAndLock;
    [SerializeField] private GameObject _mediumMazeTextAndLock;
    [SerializeField] private GameObject _impossibleMazeTextAndLock;

    private string[] _keys = new string[3] { "easyMazeUnlocked", "mediumMazeUnlocked", "impossibleMazeUnlocked" };
    private bool[] _mazeClosers = new bool[3] { true, true, true };

    private void Start()
    {
        RefreshTextAndLockObjects();
    }

    private void RefreshTextAndLockObjects()
    {
        for (int i = 0; i < _keys.Length; i++)
        {
            string key = _keys[i];
            if (PlayerPrefs.HasKey(key))
            {
                int value = PlayerPrefs.GetInt(key);
                if (value == 1)
                    _mazeClosers[i] = false;
                else
                    _mazeClosers[i] = true;
            }
            else
            {
                PlayerPrefs.SetInt(key, 0);
            }
        }

        _easyMazeTextAndLock.SetActive(_mazeClosers[0]);
        _mediumMazeTextAndLock.SetActive(_mazeClosers[1]);
        _impossibleMazeTextAndLock.SetActive(_mazeClosers[2]);
    }

    public void MazeComplete(int difficult)
    {
        if (difficult < _keys.Length)
        {
            PlayerPrefs.SetInt(_keys[difficult], 1);
            RefreshTextAndLockObjects();
        }
    }

    public void EasyMazeButtonClick()
    {
        if (_mazeClosers[0] == false)
            RandomMaze.Load(new SceneData(10, 10, 1, 2f, 30, 8, 2));
    }

    public void MediumMazeButtonClick()
    {
        if (_mazeClosers[1] == false)
            RandomMaze.Load(new SceneData(15, 15, 2, 3f, 30, 8, 8));
    }

    public void ImpossibleMazeButtonClick()
    {
        if (_mazeClosers[2] == false)
            RandomMaze.Load(new SceneData(20, 20, 3, 1f, 20, 80, 10));
    }

    public void BackButtonClick()
    {
        gameObject.SetActive(false);
    }
}
