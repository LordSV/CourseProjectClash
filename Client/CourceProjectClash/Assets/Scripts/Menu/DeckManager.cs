using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private GameObject _lockScreenCanvas;
    [SerializeField] private Card[] _cards;
    [SerializeField] private List<Card> _availableCards = new List<Card>();
    [SerializeField] private List<Card> _selectedCards = new List<Card>();
    public IReadOnlyList<Card> AvailableCards { get { return _availableCards; } }
    public IReadOnlyList<Card> SelectedCards { get { return _selectedCards; } }

    public event Action<IReadOnlyList<Card>, IReadOnlyList<Card>> UpdateAvailable;
    public event Action<IReadOnlyList<Card>> UpdateSelected;

    #region Editor
    [SerializeField] private AvailableDeckUI _availableDeckUI;
#if UNITY_EDITOR
    private void OnValidate()
    {
        _availableDeckUI.SetAllCardsCount(_cards);
    }
#endif
#endregion
    public void Init(List<int> availableCardIndexes, int[] selectedCardIndexes)
    {
        for(int i = 0; i < availableCardIndexes.Count; i++)
        {
            _availableCards.Add(_cards[availableCardIndexes[i]]);
        }
        for(int i = 0; i < selectedCardIndexes.Length; i++)
        {
            _selectedCards.Add(_cards[selectedCardIndexes[i]]);
        }
        UpdateAvailable?.Invoke(AvailableCards, SelectedCards);
        UpdateSelected?.Invoke(SelectedCards);

        _lockScreenCanvas.SetActive(false);
    }

    public void ChangesDeck(IReadOnlyList<Card> selectedCards, Action success)
    {
        _lockScreenCanvas.SetActive(true);
        int[] IDs = new int[selectedCards.Count];
        for (int i = 0; i < selectedCards.Count; i++)
        {
            IDs[i] = selectedCards[i].id;
        }
        string json = JsonUtility.ToJson(new Wrapper(IDs));
        string uri = URLLibrary.MAIN + URLLibrary.SETSELECTDECK;
        Dictionary<string, string> data = new Dictionary<string, string> { { "userID", UserInfo.Instance.ID.ToString() }, { "json", json } };
        success += () =>
        {
            for (int i = 0; i < _selectedCards.Count; i++)
            {
                _selectedCards[i] = selectedCards[i];
            }
            UpdateSelected?.Invoke(SelectedCards);
        };   
        Network.Instance.Post(uri, data, (s) => SendSuccess(s, success), Error);
    }

    private void SendSuccess(string obj, Action success)
    {
        if(obj != "ok")
        {
            Error(obj);
            return;
        }
        success?.Invoke();
        _lockScreenCanvas.SetActive(false);
    }

    private void Error(string obj)
    {
        Debug.LogError("Неудачная попытка отправки новой колоды " + obj);
        _lockScreenCanvas.SetActive(false);
    }

    [System.Serializable]
    private class Wrapper
    {
        public int[] IDs;
        public Wrapper(int[] IDs)
        {
            this.IDs = IDs;
        }
    }
}

[System.Serializable]
public class Card
{
    [field: SerializeField] public string name { get; private set; }
    [field: SerializeField] public int id { get; private set; }
    [field: SerializeField] public Sprite sprite { get; private set; }

}
