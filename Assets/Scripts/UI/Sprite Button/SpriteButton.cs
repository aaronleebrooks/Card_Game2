using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpriteButton : MonoBehaviour
{
    private PolygonCollider2D polygonCollider2D;
    private SpriteRenderer spriteRenderer;
    private Color normalColor;
    private Color hoverColor;
    private Color clickColor;
    private Color disabledColor;

    private bool isEnabled;

    public UnityEvent OnClick = new UnityEvent();

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        normalColor = spriteRenderer.color;
        hoverColor = normalColor * 0.75f;
        clickColor = normalColor * 0.5f;
        disabledColor = normalColor * 0.25f;
    }

    public void OnMouseEnter()
    {
        spriteRenderer.color = normalColor * 0.75f;
        Debug.Log("SpriteButton: Mouse entered");
    }

    public void OnMouseExit()
    {
        if(isEnabled)
        {
            spriteRenderer.color = normalColor;
            Debug.Log("SpriteButton: Mouse exited");
        }
    }

    public void OnMouseDown()
    {
        spriteRenderer.color = clickColor;
        OnClick.Invoke();
        Debug.Log("SpriteButton: Mouse down");
    }

    public void OnEnable()
    {
        spriteRenderer.color = normalColor;
        isEnabled = true;
        polygonCollider2D.enabled = true;
        Debug.Log("SpriteButton: Enabled");
    }

    public void OnDisable()
    {
        spriteRenderer.color = disabledColor;
        isEnabled = false;
        polygonCollider2D.enabled = false;
        Debug.Log("SpriteButton: Disabled");
    }
}
