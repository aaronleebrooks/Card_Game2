using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Position
    {
        Draw,
        Hand,
        Discard,
        Playfield,
        Store,
        Graveyard
    }

public class CardPositionController : MonoBehaviour
{
    public float speed = 5f; // Define the speed at which the card moves
    public Position position; // Define the position of the card

    public void OnMoveCardToThisPosition(Card card)
    {
        Debug.Log($"OnMoveCardToThisPosition: Moving card {card.name} to position {position}");
        MoveCardToThisPosition(card);
    }
    public void OnMoveCardsToThisPosition(List<Card> cards)
    {
        Debug.Log($"OnMoveCardsToThisPosition: Moving {cards.Count} cards to position {position}");
        cards.ForEach(card => MoveCardToThisPosition(card));
    }

    private void MoveCardToThisPosition(Card card)
    {
        card.DoMoveCard(this.transform, position);
    }
}