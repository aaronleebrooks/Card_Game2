using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayfieldController : MonoBehaviour
{
    private List<CardPositionController> playfieldLocations = new List<CardPositionController>();
    private List<Card> cardsInPlay = new List<Card>();
    public int numPlayfieldLocations = 5;
    public GameObject minPlayLocation;
    public GameObject maxPlayLocation;

    public void InitializePlayfieldPositions()
    {
        Debug.Log("InitializePlayfieldPositions called");
        CreatePlayfieldPositions();
    }

    public void DoPlayCard(Card card)
    {
        Debug.Log($"DoPlayCard called with card: {card.name}");
        cardsInPlay.Add(card);
    }

    public void DoSendCardToPlayfield(Card card)
    {
        Debug.Log($"DoSendCardToPlayfield called with card: {card.name}");
        cardsInPlay.Add(card);
    }

    private void CreatePlayfieldPositions()
    {
        Debug.Log("CreatePlayfieldPositions called");
        for (int i = 0; i < numPlayfieldLocations; i++)
        {
            GameObject handPosition = new GameObject("Playfield Position " + i);
            handPosition.transform.parent = transform;
            handPosition.transform.position = new Vector3(0, 0, 0);
            CardPositionController cardPositionController = handPosition.AddComponent<CardPositionController>();
            cardPositionController.enabled = false;
            playfieldLocations.Add(cardPositionController);
            Debug.Log($"Playfield position {i} created");
        }
    }
}