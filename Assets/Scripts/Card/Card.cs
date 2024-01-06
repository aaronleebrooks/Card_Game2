using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TargetType {
    Self,
    Enemy,
    EnemyCreature,
    PlayerCreature,
    PlayerCardInHand,
    Player,
    none
}
public class Card : MonoBehaviour
{
    public SO_Card cardData;
    public int cost;
    public int power;
    [SerializeField]
    private Position position;
    private bool isSelected = false;
    private bool isMoving = false;
    public UnityEvent<SO_Card> CardInitializedEvent = new UnityEvent<SO_Card>();
    public UnityEvent<Card> CardSentToDrawPile = new UnityEvent<Card>();
    public UnityEvent<Card> CardDiscarded = new UnityEvent<Card>();
    public UnityEvent<Card> CardDrawn = new UnityEvent<Card>();
    public UnityEvent<Card> CardTrashed = new UnityEvent<Card>();
    public UnityEvent<Card> CardPlayed = new UnityEvent<Card>();
    public UnityEvent<Card> CardClickedInHandEvent = new UnityEvent<Card>();
    public UnityEvent<Card> CardHoveredEvent = new UnityEvent<Card>();
    public UnityEvent<Card> CardOffHoveredEvent = new UnityEvent<Card>();
    public UnityEvent CardFaceUpEvent = new UnityEvent();
    public UnityEvent CardFaceDownEvent = new UnityEvent();
    private PlayerController owner;

    private void Awake()
    {
        Debug.Log("Awake called");
        DoInitializeCard(cardData);
    }

    public void DoInitializeCard(SO_Card card)
    {
        Debug.Log($"DoInitializeCard called with card: {card.name}");
        cardData = card;
        CardInitializedEvent.Invoke(cardData);
    }

    public void DoMoveCard(Vector3 value, Position position)
    {
        Debug.Log($"DoMoveCard called with value: {value} and position: {position}");
        GetComponent<CardMovementController>().RequestMoveCard(this as Card, value, position);
    }

    public void DoSetPosition(Position value)
    {
        Debug.Log($"DoSetPosition called with value: {value}");
        SetPosition(value);
    }

    public void DoFlipCard(bool value)
    {
        Debug.Log($"DoFlipCard called with value: {value}");
        if (value)
        {
            CardFaceUpEvent.Invoke();
        }
        else
        {
            CardFaceDownEvent.Invoke();
        }
    }

    public void DoSetMoving(bool value)
    {
        Debug.Log($"DoSetMoving called with value: {value}");
        isMoving = value;
    }

    public Position GetPosition()
    {
        Debug.Log($"GetPosition called, current position is: {position}");
        return position;
    }

    public Sprite GetCardSprite()
    {
        Debug.Log($"GetCardSprite called, current sprite is: {cardData.title}");
        if(cardData is SO_CreatureCard creatureCardData)
        {
            return creatureCardData.creatureModel;
        }
        return null;
    }

    private void SetPosition(Position value)
    {
        position = value;
    }

    public void SetIsSelected(bool value)
    {
        Debug.Log($"SetIsSelected called with value: {value}");
        isSelected = value;
    }

    public void SetOwner(PlayerController value)
    {
        Debug.Log($"DoSetOwner called with value: {value}");
        SetOwner(value);
    }

    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown called");
        switch (position)
        {
            case Position.Hand:
                CardClickedInHandEvent.Invoke(this as Card);
                break;
            case Position.Playfield:
                break;
            case Position.Discard:
                break;
            case Position.Draw:
                break;
            case Position.Store:
                break;
            case Position.Graveyard:
                break;
            default:
                break;
        }
    }

    private void OnMouseEnter()
    {
        Debug.Log("OnMouseEnter called");
        if (isSelected)
        {
            return;
        }
        CardHoveredEvent.Invoke(this as Card);
    }

    private void OnMouseExit()
    {
        Debug.Log("OnMouseExit called");
        if (isSelected)
        {
            return;
        }
        CardOffHoveredEvent.Invoke(this as Card);
    }

    public void DoSetCost(int value)
    {
        Debug.Log($"DoSetCost called with value: {value}");
        cost = value;
    }

    public void DoSetPower(int value)
    {
        Debug.Log($"DoSetPower called with value: {value}");
        power = value;
    }

    public void HideCard()
    {
        Debug.Log("HideCard called");
        gameObject.SetActive(false);
    }
}
