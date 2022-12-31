using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Deck : MonoBehaviour
{
    [SerializeField]
    private int _amountPerCardType;

    [SerializeField]
    private int _maxCurrentCards;

    [SerializeField]
    private List<GameObject> _cards = new();

    private readonly List<GameObject> _allCards = new();
    private readonly List<GameObject> _remainingCards = new();

    private readonly List<Image> _images = new();
    private HorizontalLayoutGroup _layoutGroup;

    void Start()
    {
        for (int i = 0; i < _amountPerCardType; i++) _allCards.AddRange(_cards);

        for (int i = 0; i < _allCards.Count; i++)
        {
            GameObject card = Instantiate(_allCards[i], transform);
            _images.Add(card.GetComponent<Image>());

            if (i >= _maxCurrentCards)
            {
                card.SetActive(false);
                _remainingCards.Add(card);
            }
        }
        TryGetComponent(out _layoutGroup);
    }

    private void AddCard()
    {
        if (_remainingCards.Count > 0)
        {
            int randomNumber = Random.Range(0, _remainingCards.Count - 1);
            _remainingCards[randomNumber].gameObject.SetActive(true);
            _remainingCards.Remove(_remainingCards[randomNumber]);
        }
    }

    internal void UseCard(CardView card)
    {
        Destroy(card.dragCard.gameObject);
        Destroy(card.gameObject);
        if (_remainingCards.Count == 0) _layoutGroup.spacing += 275;
        else AddCard();
    }
}