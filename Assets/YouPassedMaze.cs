using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IJunior.TypedScenes;

public class YouPassedMaze : MonoBehaviour
{
    [SerializeField] private int _difficult = 1;

    [SerializeField] private Text _coinsText;
    [SerializeField] private TimeController _timeController;

    private int _coinsCount;

    public int Difficult { set { _difficult = value; } }

    private void OnEnable()
    {
        _coinsCount += (int)(_timeController.SecUntilGameOver * 100 * _difficult)/(int)_timeController.MaxSeconds;
        _coinsText.text = _coinsCount.ToString();
    }

    public void ReturnToMenuButton()
    {
        Playground.Load(new PlaygroundData(_coinsCount, _difficult));
    }
}
