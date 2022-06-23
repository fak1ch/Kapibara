using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using System.Text;
using UnityEngine.UI;

public class GoogleServices : MonoSingleton<GoogleServices>
{
    public event Action<bool> OnAdsDeactivateChanged;
    public event Action OnAuthenticationSuccess;

    private bool _isAdsDeactivate = false;
    private bool _isSaving = true;
    private DateTime _startDateTime;

    private void Awake()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated() == false)
        {
            PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
        }
    }

    private void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            _startDateTime = DateTime.Now;
            OnAuthenticationSuccess?.Invoke();
        }
        else
        {
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }

    public void SaveDataRemoveAdsBool(bool onAdsDeactivate = false)
    {
        _isAdsDeactivate = onAdsDeactivate;
        _isSaving = true;
        OpenSavedGame("CapybaraSaves");
    }

    public void LoadData()
    {
        _isSaving = false;
        OpenSavedGame("CapybaraSaves");
    }

    private void OpenSavedGame(string filename)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
    }

    private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            if (_isSaving == true)
            {
                byte[] savedData = BitConverter.GetBytes(_isAdsDeactivate);

                SaveGame(game, savedData);
            }
            else if (_isSaving == false)
            {
                LoadGameData(game);
            }
        }
        else
        {
            // handle error
        }
    }

    private void SaveGame(ISavedGameMetadata game, byte[] savedData)
    {
        TimeSpan currentSpan = DateTime.Now - _startDateTime;
        TimeSpan totalPlaytime = game.TotalTimePlayed + currentSpan;

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
        builder = builder
            .WithUpdatedPlayedTime(totalPlaytime)
            .WithUpdatedDescription("Saved game at " + DateTime.Now);
        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);
    }

    private void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Utils.Instance.DebugLog("Saved data sucsess");
            OnAdsDeactivateChanged?.Invoke(_isAdsDeactivate);
        }
        else
        {
            // handle error
        }
    }

    private void LoadGameData(ISavedGameMetadata game)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);
    }

    private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            if (data.Length > 0 && data != null)
            {
                _isAdsDeactivate = BitConverter.ToBoolean(data, 0);
                OnAdsDeactivateChanged?.Invoke(_isAdsDeactivate);
            }
            else
            {
                Utils.Instance.DebugLog("Not have data");
            }
        }
        else
        {
            // handle error
        }
    }

    public void DeleteGameData(string filename)
    {
        // Open the file to get the metadata.
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime, DeleteSavedGame);
    }

    private void DeleteSavedGame(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.Delete(game);
        }
        else
        {
            // handle error
        }
    }
}
