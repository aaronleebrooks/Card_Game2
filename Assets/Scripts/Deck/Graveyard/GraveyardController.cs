using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GraveyardController : MonoBehaviour
{
    [SerializeField]
    List<Card> cardsInGraveyardPile = new List<Card>();

    public UnityEvent<Card> MoveGraveyardCardToGraveyardPile = new UnityEvent<Card>();

    public void DoGraveyardCard(Card card)
    {
        Debug.Log($"DoGraveyardCard called with card: {card.name}");
        cardsInGraveyardPile.Add(card);
        MoveGraveyardCardToGraveyardPile.Invoke(card);
    }

    public void DoGraveyardCards(List<Card> cards)
    {
        Debug.Log($"DoGraveyardCards called with {cards.Count} cards");
        cardsInGraveyardPile.AddRange(cards);
        cards.ForEach(card => MoveGraveyardCardToGraveyardPile.Invoke(card));
    }
    
    public void DoRemoveCardFromGraveyardPile(Card card)
    {
        Debug.Log($"DoRemoveCardFromGraveyardPile called with card: {card.name}");
        cardsInGraveyardPile.Remove(card);
    }
}