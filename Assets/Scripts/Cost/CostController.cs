using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CostController : MonoBehaviour
{
    public UnityEvent<int> CostChangedEvent = new UnityEvent<int>();
    private int cost;

    public int GetCost()
    {
        Debug.Log($"GetCost called, current cost is: {cost}");
        return cost;
    }

    private void SetCost(int value)
    {
        Debug.Log($"SetCost called with value: {value}");
        cost = value;
        CostChangedEvent.Invoke(cost);
    }

    public void DoChangeCost(int value)
    {
        Debug.Log($"DoChangeCost called with value: {value}");
        SetCost(cost + value);
    }

    public void DoSetCost(int value)
    {
        Debug.Log($"DoSetCost called with value: {value}");
        SetCost(value);
    }

    public void DoInitializeCard(SO_Card card)
    {
        Debug.Log($"DoInitializeCard called with card: {card.name}");
        SetCost(card.cost);
    }
}