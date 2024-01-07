using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class CardMovementController : MonoBehaviour
{
    public UnityEvent<Card, Vector3> OnCardMoveStart = new UnityEvent<Card, Vector3>();
    public UnityEvent<Card> OnCardMoveEnd = new UnityEvent<Card>();

    private Queue<MovementRequest> movementQueue = new Queue<MovementRequest>();
    private bool isProcessingQueue = false;

    public void RequestMoveCard(Card card, Vector3 targetPosition, Position position)
    {
        Debug.Log($"RequestMoveCard: Received request to move card {card.name} to position {targetPosition}");
        movementQueue.Enqueue(new MovementRequest(card, targetPosition, position));

        if (!isProcessingQueue)
        {
            StartCoroutine(ProcessMovementQueue());
        }
    }

    private IEnumerator ProcessMovementQueue()
    {
        Debug.Log("ProcessMovementQueue: Started processing movement queue");
        isProcessingQueue = true;

        while (movementQueue.Count > 0)
        {
            MovementRequest request = movementQueue.Dequeue();
            yield return StartCoroutine(MoveCardToPosition(request.Card, request.TargetPosition, request.Position));
        }

        Debug.Log("ProcessMovementQueue: Finished processing movement queue");
        isProcessingQueue = false;
    }

    private IEnumerator MoveCardToPosition(Card card, Vector3 targetPosition, Position position)
    {
        Debug.Log($"MoveCardToPosition: Moving card {card.name} to position {targetPosition}");
        card.DoSetMoving(true);

        Vector3 startPosition = card.transform.position;
        float startTime = Time.time; // Use Time.time instead of Time.deltaTime
        float duration = .5f;
        float closeEnoughDistance = 0.1f;
        RectTransform rectTransform = card.GetComponent<RectTransform>();
        rectTransform.pivot = new Vector2(0.5f, 0.5f);

        // Set position to center of parent
        rectTransform.anchoredPosition = Vector2.zero;

        while (Vector3.Distance(card.transform.position, targetPosition) > closeEnoughDistance)
        {
            float t = (Time.time - startTime) / duration; // Corrected time calculation
            card.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        card.transform.position = targetPosition;
        card.DoSetPosition(position);
        card.DoSetMoving(false);
        OnCardMoveEnd.Invoke(card);
        Debug.Log($"MoveCardToPosition: Finished moving card {card.name} to position {targetPosition}");
    }
}

public class MovementRequest
{
    public Card Card;
    public Vector3 TargetPosition;
    public Position Position;

    public MovementRequest(Card card, Vector3 targetPosition, Position position)
    {
        Card = card;
        TargetPosition = targetPosition;
        Position = position;
    }
}
