using System;
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

    private readonly List<GameObject> _remainingCards = new();
    private readonly List<Image> _images = new();
    private HorizontalLayoutGroup _layoutGroup;

    void Start()
    {
        for(int i = 0; i < _amountPerCardType; i++) _remainingCards.AddRange(_cards);

        AddCards(5);

        TryGetComponent<HorizontalLayoutGroup>(out _layoutGroup);
    }

    private void AddCard()
    {
        if (_remainingCards.Count != 0)
        {
            int randomNumber = Random.Range(0, _remainingCards.Count - 1);
            GameObject card = Instantiate(_remainingCards[Random.Range(0, _remainingCards.Count - 1)], transform);
            _images.Add(card.GetComponent<Image>());
            _remainingCards.RemoveAt(randomNumber);

        }
    }

    private void AddCards(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (_remainingCards.Count != 0)
            {
                int randomNumber = Random.Range(0, _remainingCards.Count - 1);
                GameObject card = Instantiate(_remainingCards[Random.Range(0, _remainingCards.Count - 1)], transform);
                _images.Add(card.GetComponent<Image>());
                _remainingCards.RemoveAt(randomNumber);
            }
        }
    }

    private void UseCard(CardView card)
    {
        if (card.Image == null) throw new NullReferenceException("No image component found");
        foreach(GameObject prefab in _cards)
        {
            if (prefab.CompareTag(card.tag))
            {
                Destroy(card);
                if (_remainingCards.Count == 0)
                {
                    _layoutGroup.spacing += 275;
                    break;
                }
                AddCard();
                break;
            }
        }
    }
}