using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayfieldPosition : MonoBehaviour
{
    public UnityEvent<PlayfieldPosition> OnPlayfieldPositionClicked = new UnityEvent<PlayfieldPosition>();
    public Sprite bluePlayfield;
    public Sprite greenPlayfield;
    public Sprite redPlayfield;
    public Sprite yellowPlayfield;
    public Sprite whitePlayfield;
    [SerializeField]
    private bool isFull = false;

    public void SetPlayfieldToBlue()
    {
        if(isFull)
        {
            return;
        }
        GetComponent<PolygonCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().sprite = bluePlayfield;
    }

    public void SetPlayfieldToDefault()
    {
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = greenPlayfield;
    }

    public void SetPlayfieldToRed()
    {
        GetComponent<SpriteRenderer>().sprite = redPlayfield;
    }

    public void SetPlayfieldToWhite()
    {
        GetComponent<SpriteRenderer>().sprite = whitePlayfield;
    }

    public void SetPlayfieldToYellow()
    {
        GetComponent<SpriteRenderer>().sprite = yellowPlayfield;
    }

    public void SetIsFull(bool value)
    {
        isFull = value;
        Debug.Log($"SetIsFull: Set isFull to {value}");
    }

    public void OnMouseDown()
    {
        if(isFull)
        {
            return;
        }
        Debug.Log("OnMouseDown: PlayfieldPosition clicked");
        OnPlayfieldPositionClicked.Invoke(this as PlayfieldPosition);
    }

    public void OnMouseEnter()
    {
        if(!isFull)
        {
            return;
        }
        Debug.Log("OnMouseEnter: Mouse entered PlayfieldPosition");
        SetPlayfieldToYellow();
    }

    public void OnMouseExit()
    {
        if(!isFull)
        {
            return;
        }
        Debug.Log("OnMouseExit: Mouse exited PlayfieldPosition");
        SetPlayfieldToBlue();
    }

    public void DoCreatureCardPlayed(Card card) 
    {
        if (card == null)
        {
            Debug.LogError("DoCreatureCardPlayed: Card is null");
            return;
        }

        CardPositionController cardPositionController = GetComponent<CardPositionController>();
        if (cardPositionController == null)
        {
            Debug.LogError("DoCreatureCardPlayed: CardPositionController is null");
            return;
        }
        cardPositionController.OnMoveCardToThisPosition(card);

        CreatureController creatureController = GetComponentInChildren<CreatureController>();
        if (creatureController == null)
        {
            Debug.LogError("DoCreatureCardPlayed: CreatureController is null");
            return;
        }
        creatureController.SetUpCreature(card);

        card.HideCard();
        SetIsFull(true);
        Debug.Log($"DoCreatureCardPlayed: Card {card.name} played on PlayfieldPosition");
    }
}
