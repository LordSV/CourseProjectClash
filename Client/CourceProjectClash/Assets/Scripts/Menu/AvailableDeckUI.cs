using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AvailableDeckUI : MonoBehaviour
{
    #region Editor
    [SerializeField] private Transform _availableCardParent;
    [SerializeField] private AvailableCardUI _availableCardUIPrefab;
    [SerializeField] private List<AvailableCardUI> _availableCardUI = new List<AvailableCardUI>();
    public void SetAllCardsCount(Card[] cards)
    {
        for(int i = 0; i < _availableCardUI.Count; i++)
        {
            Destroy(_availableCardUI[i].gameObject);
        }
        _availableCardUI.Clear();

        for(int i = 1; i < cards.Length; i++)
        {
            AvailableCardUI card = Instantiate(_availableCardUIPrefab, _availableCardParent);
            card.Init(cards[i]);
            _availableCardUI.Add(card);
        }
    }
    #endregion

}
