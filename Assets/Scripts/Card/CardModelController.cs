using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class CardModelController : MonoBehaviour
{
    public UnityEvent<string> SetName = new UnityEvent<string>();
    public UnityEvent<string> SetDescription = new UnityEvent<string>();
    public UnityEvent<string> SetFlavor = new UnityEvent<string>();

    public UnityEvent<Sprite> SetLayout = new UnityEvent<Sprite>();
    public UnityEvent<Sprite> SetBackground = new UnityEvent<Sprite>();
    public UnityEvent<Sprite> SetMainImage = new UnityEvent<Sprite>();

    public void OnCardInitialized(SO_Card card)
    {
        Debug.Log($"CardModelController: Called OnCardInitialized with card {card.name}");
        
        SetName.Invoke(card.title);
        Debug.Log($"CardModelController: SetName invoked with {card.title}");
        
        SetDescription.Invoke(card.description);
        Debug.Log($"CardModelController: SetDescription invoked with {card.description}");
        
        SetFlavor.Invoke(card.flavor);
        Debug.Log($"CardModelController: SetFlavor invoked with {card.flavor}");
        
        SetLayout.Invoke(card.cardModel);
        Debug.Log($"CardModelController: SetLayout invoked with {card.cardModel.name}");
        
        SetBackground.Invoke(card.background);
        Debug.Log($"CardModelController: SetBackground invoked with {card.background.name}");

        if (card is SO_CreatureCard creatureCard)
        {
            SetMainImage.Invoke(creatureCard.creatureModel);
            Debug.Log($"CardModelController: SetMainImage invoked with {creatureCard.creatureModel.name}");
        }
    }
}