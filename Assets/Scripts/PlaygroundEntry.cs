using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class PlaygroundEntry : MonoBehaviour, ISceneLoadHandler<PlaygroundData>
{
    [SerializeField] private CoinsWallet _wallet;
    [SerializeField] private SelectGameMode _selectGameMode;

    public void OnSceneLoaded(PlaygroundData playgroundData)
    {
        _wallet.AddBucks(playgroundData.CoinsCount);
        _selectGameMode.MazeComplete(playgroundData.Difficult);
        //StaticClass._sprites = null;
    }
}

