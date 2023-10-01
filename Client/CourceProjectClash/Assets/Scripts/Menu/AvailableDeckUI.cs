using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AvailableDeckUI : MonoBehaviour
{    
    [SerializeField] private List<AvailableCardUI> _availableCardUI = new List<AvailableCardUI>();
    #region Editor
#if UNITY_EDITOR
    [SerializeField] private Transform _availableCardParent;
    [SerializeField] private AvailableCardUI _availableCardUIPrefab;

    public void SetAllCardsCount(Card[] cards)
    {
        for(int i = 0; i < _availableCardUI.Count; i++)
        {
            GameObject go = _availableCardUI[i].gameObject;
            UnityEditor.EditorApplication.delayCall += () => DestroyImmediate(go);
        }
        _availableCardUI.Clear();

        for(int i = 1; i < cards.Length; i++)
        {
            AvailableCardUI card = Instantiate(_availableCardUIPrefab, _availableCardParent);
            card.Init(cards[i]);
            _availableCardUI.Add(card);
        }
        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif
#endregion

}
