using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EndTurnController : MonoBehaviour
{
    public Sprite endTurnButtonSprite;
    public Sprite confirmButtonSprite;
    private bool isConfirming = false;

    public UnityEvent EndTurnClicked = new UnityEvent();

    public void DoEndTurn()
    {
        Debug.Log("EndTurnController: Called DoEndTurn");
        if (isConfirming)
        {
            Debug.Log("EndTurnController: Is confirming, so ending turn");
            isConfirming = false;
            EndTurnClicked.Invoke();
            isConfirming = false;
            GetComponent<Image>().sprite = endTurnButtonSprite;
        }
        else
        {
            Debug.Log("EndTurnController: Is not confirming, so starting confirmation");
            isConfirming = true;
            GetComponent<Image>().sprite = confirmButtonSprite;
            StartCoroutine(ResetConfirmation());
        }
    }

    private IEnumerator ResetConfirmation()
    {
        yield return new WaitForSeconds(3);
        if (isConfirming)
        {
            Debug.Log("EndTurnController: Confirmation timed out, resetting button");
            isConfirming = false;
            GetComponent<Image>().sprite = endTurnButtonSprite;
        }
    }
}