using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchmakingManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuCanvas;
    [SerializeField] private GameObject _matchmakingCanvas;
    [SerializeField] private GameObject _canselButton;
    public async void FindOpponent()
    {
        _canselButton.SetActive(false);
        _mainMenuCanvas.SetActive(false);
        _matchmakingCanvas.SetActive(true);

        await MultiplayerManager.Instance.Connect();
        _canselButton.SetActive(true);
    }

    public void CanselFind()
    {
        _matchmakingCanvas.SetActive(false);
        _mainMenuCanvas.SetActive(true);

        MultiplayerManager.Instance.Leave();
    }
}
