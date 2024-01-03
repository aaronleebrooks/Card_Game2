using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerController : MonoBehaviour
{
    public UnityEvent<int> PowerChangedEvent = new UnityEvent<int>();
    private int power;

    public int GetPower()
    {
        return power;
    }

    private void SetPower(int value)
    {
        power = value;
        PowerChangedEvent.Invoke(power);
    }

    public void DoChangePower(int value)
    {
        SetPower(power + value);
    }

    public void DoSetPower(int value)
    {
        SetPower(value);
    }

    public void DoInitializeCard(SO_Card card)
    {
        SetPower(card.power);
    }
}