using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class HandController : MonoBehaviour
{
    private List<CardPositionController> handLocations = new List<CardPositionController>();
    [SerializeField]
    private List<Card> cardsInHand = new List<Card>();
    public int maxHandSize = 8;
    public GameObject minXHandLocation;
    public GameObject maxXHandLocation;

    public void Initializehand()
    {
        CreateHandPositions();
    }
    
    public void DoCardRemove(Card card)
    {
        Debug.Log($"Remove card called with card: {card.name}");
        cardsInHand.Remove(card);
        ArrangeCardsInHand();
    }

    public void DoDiscardCards(List<Card> cards)
    {
        Debug.Log($"DoDiscardCards called with {cards.Count} cards");
        cards.ForEach(card => DoCardRemove(card));
    }

    public void DoDiscardAllCards()
    {
        Debug.Log("DoDiscardAllCards called");
        cardsInHand.ForEach(card => DoCardRemove(card));
        cardsInHand.Clear();
    }

    public void DoSendCardToHand(Card card)
    {
        Debug.Log($"DoSendCardToHand called with card: {card.name}");
        cardsInHand.Add(card);
        if(cardsInHand.Count >= maxHandSize)
        {
            Debug.Log("Hand is full");
            // sendOutHandIsFullEvent(); // Implement this event if necessary
        }
        ArrangeCardsInHand();
    }

    private void ArrangeCardsInHand()
    {
        float minX = minXHandLocation.transform.position.x;
        float maxX = maxXHandLocation.transform.position.x;
        float step = (maxX - minX) / (Math.Max(1, cardsInHand.Count - 1)); // Avoid division by zero

        Debug.Log($"ArrangeCardsInHand: minX={minX}, maxX={maxX}, step={step}, cardsInHand.Count={cardsInHand.Count}");

        // Send cards to new hand locations
        for (int i = 0; i < handLocations.Count; i++)
        {
            if (i < cardsInHand.Count)
            {
                handLocations[i].gameObject.SetActive(true);
                Vector3 newPosition = new Vector3(minX + (step * i), handLocations[i].transform.position.y, handLocations[i].transform.position.z);
                handLocations[i].transform.position = newPosition;
                handLocations[i].OnMoveCardToThisPosition(cardsInHand[i]);
                cardsInHand[i].GetComponent<CardMovementController>().OnCardMoveEnd.AddListener(
                    (card) => {
                        if(card.GetPosition() != Position.Hand)
                        {
                            return;
                        }
                        card.DoFlipCard(true);
                        card.GetComponent<CardMovementController>().OnCardMoveEnd.RemoveAllListeners();
                    }
                );
                Debug.Log($"ArrangeCardsInHand: Card {i} moved to position {newPosition}");
            }
            else
            {
                handLocations[i].gameObject.SetActive(false);
                Debug.Log($"ArrangeCardsInHand: Card position {i} disabled");
            }
        }
    }

    private void CreateHandPositions()
    {
        for (int i = 0; i <= maxHandSize-1; i++)
        {
            GameObject handPosition = new GameObject("Hand Position " + i);
            handPosition.transform.parent = transform;
            handPosition.transform.position = new Vector3(minXHandLocation.transform.position.x, minXHandLocation.transform.position.y, 0);
            // Set the rotation of the hand position
            handPosition.transform.rotation = Quaternion.Euler(0, 15, 0);

            CardPositionController cardPositionController = handPosition.AddComponent<CardPositionController>();
            // Assign which position the cardPosition is in
            cardPositionController.position = Position.Hand;
            handLocations.Add(cardPositionController);
            cardPositionController.gameObject.SetActive(false);
            Debug.Log($"CreateHandPositions: Created hand position {i} at {handPosition.transform.position}");
        }

        Debug.Log($"CreateHandPositions: Created {handLocations.Count} hand positions");
    }
}