using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ManaController : MonoBehaviour
{
    private int mana = 0;

    public UnityEvent<int> ManaChangedEvent = new UnityEvent<int>();

    public int GetMana()
    {
        Debug.Log($"GetMana called, current mana is: {mana}");
        return mana;
    }

    private void SetMana(int value)
    {
        Debug.Log($"SetMana called with value: {value}");
        mana = value;
        ManaChangedEvent.Invoke(mana);
    }

    public void DoChangeMana(int value)
    {
        Debug.Log($"DoChangeMana called with value: {value}");
        SetMana(mana + value);
    }

    public void DoGainManaFromCard(Card card)
    {
        Debug.Log($"DoGainManaFromCard called with card: {card.name}");
        DoChangeMana(card.power);
    }

    public void DoLoseManaFromCard(Card card)
    {
        Debug.Log($"DoLoseManaFromCard called with card: {card.name}");
        DoChangeMana(-card.cost);
    }

    public void DoLoseManaFromSOCard(SO_Card card)
    {
        Debug.Log($"DoLoseManaFromSOCard called with card: {card.name}");
        DoChangeMana(-card.cost);
    }
}