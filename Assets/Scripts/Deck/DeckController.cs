using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeckController : MonoBehaviour
{
    public SO_Deck deckData;
    public GameObject spellCardPrefab;    
    public GameObject creatureCardPrefab;
    private List<Card> deckCards = new List<Card>();
    public UnityEvent<Card> CardInitialized = new UnityEvent<Card>();

    private void Start()
    {
        CardInitialized.AddListener((Card card) => Debug.Log($"CardInitialized triggered with card: {card.name}"));
    }

    public void DoInitializeDeck(SO_Player playerData)
    {
        deckData = playerData.deck;
        foreach (SO_Card cardData in deckData.cards)
        {
            InitializeCard(cardData);
        }
    }

    private void InitializeCard(SO_Card cardData)
    {
        GameObject cardObject = null;
        if(cardData is SO_CreatureCard creatureCardData)
        {
            cardObject = Instantiate(creatureCardPrefab);
        }
        else if(cardData is SO_SpellCard spellCardData)
        {
            cardObject = Instantiate(spellCardPrefab);
        }

        Card card = cardObject.GetComponent<Card>();
        CardInitialized.Invoke(card);
        card.DoInitializeCard(cardData);
        deckCards.Add(card);
    }

    public void AddCardToDeck(SO_Card cardData)
    {
        InitializeCard(cardData);
    }

    public void AddCardToDeck(Card card)
    {
        deckCards.Add(card);
    }
}