using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public void OnSpriteInitialized(Sprite sprite)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnSpriteToggled(bool value)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = value;
    }
}
