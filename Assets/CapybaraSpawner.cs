using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapybaraSpawner : MonoBehaviour
{
    [SerializeField] private UIController _uiController;
    [SerializeField] private TimeController _timeController;
    [SerializeField] private GameObject[] _capybaraPrefabs;

    [SerializeField] private Transform[] _spawnPoints;

    private void OnEnable()
    {
        _timeController.OnLastTenSeconds += SpawnCapybariesAtAllPoints;
        _timeController.OnMinuteLater += SpawnRandomCapybaraAtRandomSpawnPoint;
        _timeController.OnGamePassed += GamePassed;
        _uiController.OnGameStart += GameStart;
    }

    private void OnDisable()
    {
        _timeController.OnLastTenSeconds -= SpawnCapybariesAtAllPoints;
        _timeController.OnMinuteLater -= SpawnRandomCapybaraAtRandomSpawnPoint;
        _timeController.OnGamePassed -= GamePassed;
        _uiController.OnGameStart -= GameStart;
    }

    private void GamePassed()
    {
        Capybara[] capybaries = FindObjectsOfType<Capybara>();
        for(int i=0; i<capybaries.Length; i++)
        {
            capybaries[i].gameObject.SetActive(false);
        }
    }

    private void GameStart()
    {

    }

    private void SpawnRandomCapybaraAtRandomSpawnPoint()
    {
        int indexCapybara = Random.Range(0, _capybaraPrefabs.Length);
        int indexSpawnPoint = Random.Range(0, _spawnPoints.Length);

        Instantiate(_capybaraPrefabs[indexCapybara], _spawnPoints[indexSpawnPoint].position, Quaternion.identity);
    }

    private void SpawnCapybariesAtAllPoints()
    {
        int indexCapybara;
        if (_timeController.SecUntilGameOver <= 10)
        {
            foreach (Transform spawnPoint in _spawnPoints)
            {
                indexCapybara = Random.Range(0, _capybaraPrefabs.Length);
                Instantiate(_capybaraPrefabs[indexCapybara], spawnPoint.position, Quaternion.identity);
            }
        }
    }
}
