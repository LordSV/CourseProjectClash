using UnityEngine;
using UnityEngine.UI;

public class AvailableCardUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Text _text;

    public void Init(Card card)
    {
        _image.sprite = card.sprite;
        _text.text = card.name;
    }
}
