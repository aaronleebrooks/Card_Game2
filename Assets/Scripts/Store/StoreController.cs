using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreController : MonoBehaviour
{
    public SO_Deck storeDeck;
    private int defaultStock = 10;
    private Dictionary<int, int> cardStock;
    public List<Card> cardsInStore = new List<Card>();
    public GameObject storeModal;
    public GameObject creatureCardPrefab;
    public GameObject spellCardPrefab;
    public GameObject shelfPrefab;

    public void InitializeStoreDeck(SO_Deck deck)
    {
        storeDeck = deck;
    }

    private void InitializeCardStock(SO_Card cardData)
    {
        cardStock.Add(cardData.typeId, defaultStock);
    }

    private Card InitializeCard(SO_Card cardData)
    {
        GameObject cardObject = null;
        if (cardData is SO_CreatureCard creatureCardData)
        {
            cardObject = Instantiate(creatureCardPrefab);
        }
        else if (cardData is SO_SpellCard spellCardData)
        {
            cardObject = Instantiate(spellCardPrefab);
        }

        Card card = cardObject.GetComponent<Card>();
        card.DoInitializeCard(cardData);
        cardsInStore.Add(card);
        return card;
    }

    private void InitializeShelf()
    {
        foreach (SO_Card cardData in storeDeck.cards)
        {
            Card newCard = InitializeCard(cardData);
        }
    }

    public void DoBuyCard()
    {
        Debug.Log($"StoreController: Called DoBuyCard with card");
    }

    public void ToggleStoreModal(bool value)
    {
        Debug.Log($"StoreController: Called ToggleStoreModal with value {value}");
        storeModal.SetActive(value);
    }   
}
