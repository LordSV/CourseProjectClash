using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private Card[] _cards;
    [SerializeField] private List<Card> _availableCards = new List<Card>();
    [SerializeField] private List<Card> _selectedCards = new List<Card>();
    public IReadOnlyList<Card> AvailableCards { get { return _availableCards; } }
    public IReadOnlyList<Card> SelectedCards { get { return _selectedCards; } }

    public event Action<IReadOnlyList<Card>> UpdateAvailable;
    public event Action<IReadOnlyList<Card>> UpdateSelected;

    #region Editor
    [SerializeField] private AvailableDeckUI _availableDeckUI;
    private void OnValidate()
    {
        _availableDeckUI.SetAllCardsCount(_cards);
    }
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
        UpdateAvailable?.Invoke(AvailableCards);
        UpdateSelected?.Invoke(SelectedCards);
    }
}

[System.Serializable]
public class Card
{
    [field: SerializeField] public string name { get; private set; }
    [field: SerializeField] public Sprite sprite { get; private set; }
}
