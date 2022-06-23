using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData
{
    private int _mazeWidth;
    private int _mazeHeight;
    private int _difficult;
    private float _maxGameTime;
    private int _secondsUntilSpawnCapybara;
    private int _capybaraAccelaration;
    private int _capybaraSpeed;

    public int MazeWidth => _mazeWidth;
    public int MazeHeight => _mazeHeight;
    public int Difficult => _difficult;
    public float MaxGameTime => _maxGameTime;
    public int SecondsUntilSpawnCapybara => _secondsUntilSpawnCapybara;
    public int CapybaraAccelaration => _capybaraAccelaration;
    public int CapybaraSpeed => _capybaraSpeed;

    public SceneData(int mazeWidth, int mazeHeight, int difficult, float maxGameTime, int secondsUntilSpawnCapybara, int capybaraAccelaration, int capybaraSpeed)
    {
        _mazeWidth = mazeWidth;
        _mazeHeight = mazeHeight;
        _difficult = difficult;
        _maxGameTime = maxGameTime;
        _secondsUntilSpawnCapybara = secondsUntilSpawnCapybara;
        _capybaraAccelaration = capybaraAccelaration;
        _capybaraSpeed = capybaraSpeed;
    }
}
