using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayfieldPosition : MonoBehaviour
{
    public UnityEvent OnPlayfieldPositionClicked = new UnityEvent();
    public UnityEvent OnPlayfieldPositionHovered = new UnityEvent();
    public UnityEvent OnPlayfieldPositionOffHovered = new UnityEvent();
    public Sprite bluePlayfield;
    public Sprite greenPlayfield;
    public Sprite redPlayfield;
    public Sprite yellowPlayfield;
    public Sprite whitePlayfield;
    void Start()
    {
        gameObject.AddComponent<SpriteRenderer>();
        gameObject.AddComponent<BoxCollider2D>();
        SetPlayfieldToDefault();
    }
    public void GetSpriteFromCard(Card card)
    {
        GetComponent<SpriteRenderer>().sprite = card.GetCardSprite();
    }

    public void SetPlayfieldToBlue()
    {
        GetComponent<SpriteRenderer>().sprite = bluePlayfield;
    }

    public void SetPlayfieldToDefault()
    {
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
}
