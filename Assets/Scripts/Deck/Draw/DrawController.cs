using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DrawController : MonoBehaviour
{
    public UnityEvent OnDrawPileEmpty = new UnityEvent();
    public UnityEvent<Card> OnCardDrawn = new UnityEvent<Card>();
    public UnityEvent<Card> MoveDrawCardToDrawPile = new UnityEvent<Card>();


    [SerializeField]
    List<Card> cardsInDrawPile = new List<Card>();
    bool isShuffling = false;

    public void DoAddCardToDrawPile(Card card)
    {
        cardsInDrawPile.Add(card);
        card.DoFlipCard(false);
        card.transform.parent = transform;
        MoveDrawCardToDrawPile.Invoke(card);
    }

    public void DoAddCardsToDrawPile(List<Card> cards)
    {
        cards.ForEach(card => DoAddCardToDrawPile(card));
    }

    public void StartShuffleDrawPile()
    {
        StartCoroutine(DoShuffleDrawPile());
    }

    public void DoDrawCards(int numberOfCards)
   {
        for (int i = numberOfCards; i > 0; i--)
        {
            Debug.Log($"Drawing card number: {i} cardsInDrawPile.Count: {cardsInDrawPile.Count}");
            if (cardsInDrawPile.Count == 0)
            {
                Debug.Log("Draw pile is empty");
                isShuffling = true;
                StartCoroutine(WaitForDeckToBeShuffled(i));
                OnDrawPileEmpty.Invoke();
                break;
            }

            Card card = cardsInDrawPile[0];
            OnCardDrawn.Invoke(card);
            cardsInDrawPile.RemoveAt(0);
        }
    }

    private IEnumerator WaitForDeckToBeShuffled(int remainingCards)
    {
        Debug.Log($"Waiting for deck to be shuffled, remaining cards: {remainingCards}");
        yield return new WaitUntil(() => !isShuffling);
        DoDrawCards(remainingCards);
    }

    public void DoRemoveAllCardsFromDrawPile()
    {
        Debug.Log("DoRemoveAllCardsFromDrawPile called");
        cardsInDrawPile.Clear();
    }

    public IEnumerator DoShuffleDrawPile()
    {
        Debug.Log("DoShuffleDrawPile called");
        for (int i = 0; i < cardsInDrawPile.Count; i++)
        {
            Card temp = cardsInDrawPile[i];
            int randomIndex = Random.Range(i, cardsInDrawPile.Count);
            cardsInDrawPile[i] = cardsInDrawPile[randomIndex];
            cardsInDrawPile[randomIndex] = temp;
            yield return null;
        }
        isShuffling = false;
        Debug.Log("Shuffling completed");
    }
}
