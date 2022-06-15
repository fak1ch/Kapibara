using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private Player _player;
    [SerializeField] private Button _showAdButton;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    private string _adUnitId = null;
    private bool _rewardedVideoReadyToShow = false;

    public Button ShowAdButton => _showAdButton;

    void Awake()
    {
        _adUnitId = _androidAdUnitId;
    }

    public void LoadAd()
    {
        Utils.Instance.DebugLog("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    public void ShowAd()
    {
        Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId))
        {
            Utils.Instance.DebugLog("Unity Ads Rewarded Ad Completed");

            _player.gameObject.SetActive(true);
            _player.RespawnPlayerHere();
        }
    }

    #region Interface Implementations

    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (placementId == _adUnitId)
        {
            Utils.Instance.DebugLog("Ad Loaded: " + _adUnitId);
            _rewardedVideoReadyToShow = true;
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Utils.Instance.DebugLog($"Load Failed: [{error}:{placementId}] {message}");
        LoadAd();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Utils.Instance.DebugLog($"OnUnityAdsShowFailure: [{error}]: {message}");
        ShowAd();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Utils.Instance.DebugLog($"OnUnityAdsShowStart: {placementId}");
        _rewardedVideoReadyToShow = false;
        LoadAd();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Utils.Instance.DebugLog($"OnUnityAdsShowClick: {placementId}");
    }

    //public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    //{
    //    Utils.Instance.DebugLog($"OnUnityAdsShowComplete: [{showCompletionState}]: {placementId}");
    //}
    #endregion

    void OnDestroy()
    {
        // Clean up the button listeners:
        _showAdButton.onClick.RemoveAllListeners();
    }
}