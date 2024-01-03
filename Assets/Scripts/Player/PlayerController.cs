using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public SO_Player playerData;
    private int mana;
    public UnityEvent<SO_Player> PlayerInitialized = new UnityEvent<SO_Player>();
    public UnityEvent<Card> PlayerCardInitialized = new UnityEvent<Card>();
    public UnityEvent PlayerTurnStarted = new UnityEvent();
    public UnityEvent PlayerTurnMain = new UnityEvent();
    public UnityEvent PlayerTurnEnded = new UnityEvent();
    public UnityEvent<Card> PlayerSelectedCardCanBeDiscarded = new UnityEvent<Card>();
    public UnityEvent<Card> PlayerSelectedCardCanBePlayed = new UnityEvent<Card>();
    public UnityEvent<Card> PlayerDiscardedCardForMana = new UnityEvent<Card>();
    public UnityEvent<Card> PlayerDiscardedCard = new UnityEvent<Card>();
    public UnityEvent<Card> PlayerDrewCard = new UnityEvent<Card>();
    public UnityEvent<Card> PlayerSentCardToDrawPile = new UnityEvent<Card>();
    public UnityEvent PlayerDrewHand = new UnityEvent();
    public UnityEvent ResetSelectionHighlights = new UnityEvent();
    private Card selectedCardInHand;

    private void Start()
    {
        PlayerInitialized.AddListener((_) => Debug.Log("PlayerInitialized triggered"));
        PlayerCardInitialized.AddListener((card) => Debug.Log($"PlayerCardInitialized triggered with card {card.name}"));
        PlayerTurnStarted.AddListener(() => Debug.Log("PlayerTurnStarted triggered"));
        PlayerTurnMain.AddListener(() => Debug.Log("PlayerTurnMain triggered"));
        PlayerTurnEnded.AddListener(() => Debug.Log("PlayerTurnEnded triggered"));
        PlayerSelectedCardCanBeDiscarded.AddListener((card) => Debug.Log($"PlayerSelectedCardCanBeDiscarded triggered with card {card.name}"));
        PlayerSelectedCardCanBePlayed.AddListener((card) => Debug.Log($"PlayerSelectedCardCanBePlayed triggered with card {card.name}"));
        PlayerDiscardedCardForMana.AddListener((card) => Debug.Log($"PlayerDiscardedCardForMana triggered with card {card.name}"));
        ResetSelectionHighlights.AddListener(() => Debug.Log("ResetSelectionHighlights triggered"));
    }
    public void DoSelectCardInHand(Card card)
    {
        if (selectedCardInHand != null)
        {
            DoDeselectCardInHand(selectedCardInHand);
            if (selectedCardInHand == card)
            {
                selectedCardInHand = null;
                return;
            }
        }
        card.SetIsSelected(true);
        HighlightController highlight = card.GetComponentInChildren<HighlightController>(card);
        highlight.OnHighlightChanged("yellow");
        selectedCardInHand = card;
        LookForSelectedCardTargets();
    }

    public void DoDeselectCardInHand(Card card)
    {
        card.SetIsSelected(false);
        HighlightController highlight = card.GetComponentInChildren<HighlightController>(card);
        highlight.OnHighlightChanged("none");
        highlight.OnUnzoomHighlight(card);
        ResetSelectionHighlights.Invoke();
    }

    public void DoInitializePlayer()
    {
        PlayerInitialized.Invoke(playerData);
    }

    public void DoInitializePlayerCard(Card card)
    {
        card.CardClickedInHandEvent.AddListener(DoSelectCardInHand);
        PlayerDiscardedCardForMana.AddListener((card) => card.CardDiscarded.Invoke(card));
        PlayerDiscardedCard.AddListener((card) => card.CardDiscarded.Invoke(card));
        PlayerDrewCard.AddListener((card) => card.CardDrawn.Invoke(card));
        PlayerSentCardToDrawPile.AddListener((card) => card.CardSentToDrawPile.Invoke(card));
        PlayerSentCardToDrawPile.Invoke(card);
        PlayerCardInitialized.Invoke(card);
    }

    public void DoDiscardCard()
    {
        if (selectedCardInHand == null)
        {
            return;
        }
        PlayerDiscardedCardForMana.Invoke(selectedCardInHand);
        DoDeselectCardInHand(selectedCardInHand);
    }

    private void LookForSelectedCardTargets()
    {
        if (selectedCardInHand == null)
        {
            return;
        }
        PlayerSelectedCardCanBeDiscarded.Invoke(selectedCardInHand);
        if (selectedCardInHand.cost <= mana)
        {
            PlayerSelectedCardCanBePlayed.Invoke(selectedCardInHand);
        }
    }

    public void DoStartTurn()
    {
        PlayerTurnStarted.Invoke();
    }

    public void DoMainTurn()
    {
        PlayerTurnMain.Invoke();
    }

    public void DoEndTurn()
    {
        PlayerTurnEnded.Invoke();
    }

    public void DoDrawCard(Card card)
    {
        PlayerDrewCard.Invoke(card);
    }
}
