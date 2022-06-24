using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeLots : MonoBehaviour
{
    [SerializeField] private CustomizeLot[] _lots;
    [SerializeField] private Transform[] _lotSpawnPoints;

    [SerializeField] private CustomizeLot _firstTerrainLot;
    [SerializeField] private bool _isTerrains = false;

    [SerializeField] private List<CustomizeLot> _spawnedLots = new List<CustomizeLot>();

    private bool _isOn = true;

    private void Start()
    {
        if (_isOn == true)
        {
            _isOn = false;
            GameObject lot = Instantiate(_lots[0].gameObject, _lotSpawnPoints[0]);
            lot.transform.SetParent(this.gameObject.transform);
            var script = lot.GetComponent<CustomizeLot>();
            _spawnedLots.Add(script);
        }
    }

    public void AddLot(int id)
    {
        if (_isOn == true && _isTerrains == true)
        {
            _isOn = false;
            GameObject lot = Instantiate(_lots[0].gameObject, _lotSpawnPoints[0]);
            lot.transform.SetParent(this.gameObject.transform);
            var script = lot.GetComponent<CustomizeLot>();
            _spawnedLots.Add(script);
        }

        for (int i = 0; i < _lots.Length; i++)
        {
            if (id == _lots[i].Id)
            {
                GameObject lot = Instantiate(_lots[i].gameObject, _lotSpawnPoints[_spawnedLots.Count]);
                lot.transform.SetParent(this.gameObject.transform);
                var script = lot.GetComponent<CustomizeLot>();
                _spawnedLots.Add(script);
            }
        }
    }

    public List<CustomizeLot> GetSelectedLots()
    {
        List<CustomizeLot> result = new List<CustomizeLot>(_spawnedLots);

        if (_isTerrains)
        {
            if (result.Count == 1)
                result.Add(_spawnedLots[0]);
            else
                result.Add(_spawnedLots[Random.Range(0, _spawnedLots.Count)]);
        }

        return result;
    }

    public void ApplyMassives()
    {
        var list = GetSelectedLots();

        if (_isTerrains == true)
        {
            StaticClass._groundMaterial = list[0]._groundMaterial;
            StaticClass._wallMaterial = list[0]._wallMaterial;
        }
        else
        {
            List<Sprite> sprites = new List<Sprite>();
            for (int i = 0; i < list.Count; i++)
            {
                sprites.Add(list[i]._sprite);
            }
        }
    }
}
