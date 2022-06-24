using UnityEngine;
using UnityEngine.UI;

public class CoinsWallet : MonoBehaviour
{
    [SerializeField] private Text _bucksText;
    [SerializeField] private Text _bucksTextSecond;

    private int _bucks = 0;

    public int GetNumberOfBucks => _bucks;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("bucks") == true)
        {
            _bucks = PlayerPrefs.GetInt("bucks");
        }
        RefreshBucksText();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            _bucks += 1000;
            RefreshBucksText();
        }
    }

    public void AddBucks(int bucksCount)
    {
        if (PlayerPrefs.HasKey("bucks") == true)
        {
            _bucks = PlayerPrefs.GetInt("bucks");
        }

        if (bucksCount > 0)
        {
            _bucks += bucksCount;
            RefreshBucksText();
        }
    }

    public bool TakeBucks(int bucksCount)
    {
        if (bucksCount > 0)
        {
            if (_bucks >= bucksCount)
            {
                _bucks -= bucksCount;
                RefreshBucksText();
                return true;
            }
        }

        return false;
    }

    private void RefreshBucksText()
    {
        _bucksText.text = _bucks.ToString();
        _bucksTextSecond.text = _bucks.ToString();
        PlayerPrefs.SetInt("bucks", _bucks);
    }
}
