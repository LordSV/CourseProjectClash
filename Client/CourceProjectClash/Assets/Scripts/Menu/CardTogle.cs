using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTogle : MonoBehaviour
{
    [SerializeField] private CardSelector _selector;
    [SerializeField] private int _index;

    public void Click(bool value)
    {
        if (value == false) return;
        _selector.SetSelectTogleIndex(_index);
    }
}
