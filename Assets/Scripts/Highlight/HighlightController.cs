using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HighlightController : MonoBehaviour
{
    public Sprite whiteHighlight;
    public Sprite redHighlight;
    public Sprite greenHighlight;
    public Sprite blueHighlight;
    public Sprite purpleHighlight;
    public Sprite yellowHighlight;
    private SpriteRenderer spriteRenderer;

    private Vector3 cardScale;
    

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnHighlightChanged(string color)
    {
        Debug.Log($"HighlightController: Called OnHighlightChanged with color {color}");
        switch (color)
        {
            case "white":
                spriteRenderer.sprite = whiteHighlight;
                break;
            case "red":
                spriteRenderer.sprite = redHighlight;
                break;
            case "green":
                spriteRenderer.sprite = greenHighlight;
                break;
            case "blue":
                spriteRenderer.sprite = blueHighlight;
                break;
            case "yellow":
                spriteRenderer.sprite = yellowHighlight;
                break;            
            case "purple":
                spriteRenderer.sprite = yellowHighlight;
                break;
            default:
                spriteRenderer.sprite = null;
                break;
        }
    }

    public void OnZoomHighlight(Card card)
    {
        Debug.Log($"HighlightController: Called OnZoomHighlight with card {card.name}");
        cardScale = card.transform.localScale;
        card.transform.localScale = cardScale * 1.25f;
        card.GetComponent<SortingGroup>().sortingOrder += 2;
    }

    public void OnUnzoomHighlight(Card card)
    {
        Debug.Log($"HighlightController: Called OnUnzoomHighlight with card {card.name}");
        card.transform.localScale = cardScale;
        card.GetComponent<SortingGroup>().sortingOrder -= 2;
    }
}
