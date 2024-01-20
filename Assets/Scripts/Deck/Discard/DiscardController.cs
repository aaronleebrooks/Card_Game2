using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DiscardController : MonoBehaviour
{
    [SerializeField]
    List<Card> cardsInDiscardPile = new List<Card>();

    public UnityEvent<Card> MoveDiscardCardToDiscardPile = new UnityEvent<Card>();

    public UnityEvent<List<Card>> SendAllDiscardsToDrawPile = new UnityEvent<List<Card>>();

    public void DoDiscardCard(Card card)
    {
        Debug.Log($"DoDiscardCard called with card: {card.name}");
        cardsInDiscardPile.Add(card);
        card.GetComponent<BoxCollider2D>().enabled = false;
        card.transform.parent = transform;
        MoveDiscardCardToDiscardPile.Invoke(card);
    }

    public void DoDiscardCards(List<Card> cards)
    {
        Debug.Log($"DoDiscardCards called with {cards.Count} cards");
        cards.ForEach(card => DoDiscardCard(card));
    }
    
    public void DoRemoveCardFromDiscardPile(Card card)
    {
        Debug.Log($"DoRemoveCardFromDiscardPile called with card: {card.name}");
        cardsInDiscardPile.Remove(card);
    }

    public void DoRemoveAllCardsFromDiscardPile()
    {
        Debug.Log("DoRemoveAllCardsFromDiscardPile called");
        SendAllDiscardsToDrawPile.Invoke(cardsInDiscardPile);
        cardsInDiscardPile.Clear();
    }
}