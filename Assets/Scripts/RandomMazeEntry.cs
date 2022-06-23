using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class RandomMazeEntry : MonoBehaviour, ISceneLoadHandler<SceneData>
{
    [SerializeField] private MazeSpawner _mazeSpawner;
    [SerializeField] private YouPassedMaze _youPassedMaze;
    [SerializeField] private TimeController _timeController;
    [SerializeField] private CapybaraSpawner _capybaraSpawner;

    public void OnSceneLoaded(SceneData sceneData)
    {
        _youPassedMaze.Difficult = sceneData.Difficult;
        _timeController.MaxSeconds = sceneData.MaxGameTime;
        _timeController.SecondsUntilSpawnCapybara = sceneData.SecondsUntilSpawnCapybara;
        _capybaraSpawner.CapybaraAccelaration = sceneData.CapybaraAccelaration;
        _capybaraSpawner.CapybaraSpeed = sceneData.CapybaraSpeed;

        _mazeSpawner.AfterSceneLoader(sceneData.MazeWidth, sceneData.MazeHeight);
    }
}
