using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private DeckLoader _deckLoader;
    private void Start()
    {
        _deckLoader.Init();
    }

}
