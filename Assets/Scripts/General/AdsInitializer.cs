using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoSingleton<AdsInitializer>, IUnityAdsInitializationListener
{
    [SerializeField] private RewardedAdsButton _rewardedAdsButton;

    [SerializeField] private string _androidGameId = "4763307";
    [SerializeField] private string _iOSGameId = "4763306";
    [SerializeField] private bool _testMode = false;
    private string _gameId;

    void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
        if (Advertisement.isSupported)
        {
            _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? _iOSGameId
                : _androidGameId;
            Utils.Instance.DebugLog(Application.platform + " supported by Advertisement " + _gameId);
            Advertisement.Initialize(_gameId, _testMode, this);
        }
        else
        {
            Utils.Instance.DebugLog(Application.platform + "NOT supported by Advertisement");
        }
    }

    #region Interface Implementations
    public void OnInitializationComplete()
    {
        Utils.Instance.DebugLog("Init Success");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Utils.Instance.DebugLog($"Init Failed: [{error}]: {message}");
        Advertisement.Initialize(_gameId, _testMode, this);
    }
    #endregion
}