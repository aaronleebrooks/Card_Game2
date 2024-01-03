using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardPileController : MonoBehaviour
{
    public UnityEvent CardPileClicked = new UnityEvent();
    public UnityEvent CardPileHovered = new UnityEvent();
    public UnityEvent CardPileOffHovered = new UnityEvent();


    private void OnMouseEnter()
    {
        Debug.Log("CardPile.OnMouseEnter");
        CardPileHovered.Invoke();
    }

    private void OnMouseExit()
    {
        Debug.Log("CardPile.OnMouseExit");
        CardPileOffHovered.Invoke();
    }

    private void OnMouseDown()
    {
        Debug.Log("CardPile.OnMouseDown");
        CardPileClicked.Invoke();
    }
}
