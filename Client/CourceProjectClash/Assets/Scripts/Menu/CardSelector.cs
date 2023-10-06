using System;
using System.Collections.Generic;
using UnityEngine;

public class CardSelector : MonoBehaviour
{
    [SerializeField] private DeckManager _deckManager;
    [SerializeField] private AvailableDeckUI _availableDeckUI;
    [SerializeField] private SelectedDeckUI _selectedDeckUI;
    [Space]
    [Header("Логика преключения канваса")]
    [SerializeField] private GameObject _mainCanvas;
    [SerializeField] private GameObject _cardSelectCanvas;
    private List<Card> _availableCards = new List<Card>();
    private List<Card> _selectedCards = new List<Card>();
    public IReadOnlyList<Card> AvailableCards { get { return _availableCards; } }
    public IReadOnlyList<Card> SelectedCards { get { return _selectedCards; } }
    private int _selectTogleIndex = 0;

    private void OnEnable()
    {
        FillListFromManager();
    }

    private void FillListFromManager()
    {
        _availableCards.Clear();
        for (int i = 0; i < _deckManager.AvailableCards.Count; i++)
        {
            _availableCards.Add(_deckManager.AvailableCards[i]);
        }
        _selectedCards.Clear();
        for (int i = 0; i < _deckManager.SelectedCards.Count; i++)
        {
            _selectedCards.Add(_deckManager.SelectedCards[i]);
        }
    }

    public void SetSelectTogleIndex(int index)
    {
        _selectTogleIndex = index;
    }

    public void SelectCard(int cardID)
    {
        _selectedCards[_selectTogleIndex] = _availableCards[cardID-1];
        _selectedDeckUI.UpdateCardsList(SelectedCards);
        _availableDeckUI.UpdateCardsList(AvailableCards, SelectedCards);
    }

    public void SaveChanges()
    {
        _deckManager.ChangesDeck(SelectedCards, CloseChangesWindow);
    }

    public void CanselChange()
    {
        FillListFromManager();
        _selectedDeckUI.UpdateCardsList(SelectedCards);
        _availableDeckUI.UpdateCardsList(AvailableCards, SelectedCards);
        CloseChangesWindow();
    }
    public void CloseChangesWindow()
    {
        _cardSelectCanvas.SetActive(false);
        _mainCanvas.SetActive(true);
    }
}
