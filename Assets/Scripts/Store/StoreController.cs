using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class StoreController : MonoBehaviour
{
    public SO_Deck storeDeck;
    private int defaultStock = 10;
    private Dictionary<int, int> cardStock = new Dictionary<int, int>();
    public List<Card> cardsInStore = new List<Card>();

    public List<ShelfController> shelves = new List<ShelfController>();

    public UnityEvent<bool> StoreModalToggledEvent = new UnityEvent<bool>();
    public UnityEvent<Card> CardBoughtEvent = new UnityEvent<Card>();
    public GameObject storeModal;
    public GameObject creatureCardPrefab;
    public GameObject spellCardPrefab;
    public GameObject shelfPrefab;
    public GameObject shelvesGrid;
    
    public void InitializeStore(SO_Deck deck)
    {
        Debug.Log("InitializeStore: Initializing store with deck");
        if (deck == null)
        {
            Debug.LogError("InitializeStore: Deck is null");
            return;
        }
        InitializeStoreDeck(deck);
    }

    private void InitializeStoreDeck(SO_Deck deck)
    {
        Debug.Log("InitializeStoreDeck: Initializing store deck");
        storeDeck = deck;
        foreach (SO_Card cardData in storeDeck.cards)
        {
            if (cardData == null)
            {
                Debug.LogError("InitializeStoreDeck: Card data is null");
                continue;
            }
            InitializeShelf(cardData);
        }
    }

    private void InitializeCardStock(SO_Card cardData)
    {
        Debug.Log($"InitializeCardStock: Initializing card stock for card {cardData.name}");
        cardStock.Add(cardData.typeId, defaultStock);
    }

    private Card InitializeCard(SO_Card cardData)
    {
        Debug.Log($"InitializeCard: Initializing card {cardData.name}");
        GameObject cardObject = null;
        if (cardData is SO_CreatureCard creatureCardData)
        {
            cardObject = Instantiate(creatureCardPrefab);
        }
        else if (cardData is SO_SpellCard spellCardData)
        {
            cardObject = Instantiate(spellCardPrefab);
        }

        if (cardObject == null)
        {
            Debug.LogError("InitializeCard: Card object is null");
            return null;
        }
        cardObject.transform.localPosition = Vector3.zero; // Set the local position to zero
        Card card = cardObject.GetComponent<Card>();
        if (card == null)
        {
            Debug.LogError("InitializeCard: Card component is null");
            return null;
        }

        card.DoInitializeCard(cardData);
        cardsInStore.Add(card);
        return card;
    }
    private void InitializeShelf(SO_Card cardData)
    {
        Debug.Log($"InitializeShelf: Initializing shelf for card {cardData.name}");
        GameObject shelfObject = Instantiate(shelfPrefab);
        if (shelfObject == null)
        {
            Debug.LogError("InitializeShelf: Shelf object is null");
            return;
        }
        shelfObject.transform.SetParent(shelvesGrid.transform, false);
        ShelfController shelfController = shelfObject.GetComponent<ShelfController>();
        if (shelfController == null)
        {
            Debug.LogError("InitializeShelf: ShelfController component is null");
            return;
        }

        shelfController.InitializeShelfCard(InitializeCard(cardData), defaultStock);
        shelfController.SetTypeId(cardData.typeId);
        shelfController.CardBoughtEvent.AddListener((Card card) => DoBuyCard(card));
        StoreModalToggledEvent.AddListener((bool value) => shelfController.ToggleMask(value));
        InitializeCardStock(cardData);
        shelves.Add(shelfController);
    }

    public void DoBuyCard(Card card)
    {
        Debug.Log($"DoBuyCard: Buying card {card.name}");
        CardBoughtEvent.Invoke(card);
    }

    public void ToggleStoreModal(bool value)
    {
        Debug.Log($"ToggleStoreModal: Toggling store modal to {value}");
        StoreModalToggledEvent.Invoke(value);
    }

    public void CheckIfCanBuy(int mana)
    {
        Debug.Log($"CheckIfCanBuy: Checking if can buy with mana {mana}");
        foreach (ShelfController shelf in shelves)
        {
            shelf.CheckIfCanBuy(mana);
        }
    }

}
