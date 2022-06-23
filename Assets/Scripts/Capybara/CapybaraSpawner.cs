using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CapybaraSpawner : MonoBehaviour
{
    [SerializeField] private UIController _uiController;
    [SerializeField] private TimeController _timeController;
    [SerializeField] private GameObject[] _capybaraPrefabs;

    [SerializeField] private Transform[] _spawnPoints;

    [SerializeField] private bool _defaultGameMode = true;

    private void OnEnable()
    {
        if (_defaultGameMode)
        {
            _timeController.OnLastTenSeconds += SpawnCapybariesAtAllPoints;
        }
        _timeController.OnMinuteLater += SpawnRandomCapybaraAtRandomSpawnPoint;
        _timeController.OnTimeOver += TimeOver;
    }

    private void OnDisable()
    {
        if (_defaultGameMode)
        {
            _timeController.OnLastTenSeconds -= SpawnCapybariesAtAllPoints;
        }
        _timeController.OnMinuteLater -= SpawnRandomCapybaraAtRandomSpawnPoint;
        _timeController.OnTimeOver -= TimeOver;
    }

    private void TimeOver()
    {
        Capybara[] capybaries = FindObjectsOfType<Capybara>();
        for(int i=0; i<capybaries.Length; i++)
        {
            capybaries[i].gameObject.SetActive(false);
        }
    }

    private void SpawnRandomCapybaraAtRandomSpawnPoint()
    {
        int indexCapybara = Random.Range(0, _capybaraPrefabs.Length);
        int indexSpawnPoint = Random.Range(0, _spawnPoints.Length);

        var capybara = Instantiate(_capybaraPrefabs[indexCapybara], _spawnPoints[indexSpawnPoint].position, Quaternion.identity);

        if (_defaultGameMode == false)
            capybara.GetComponent<NavMeshAgent>().acceleration = 80;
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
