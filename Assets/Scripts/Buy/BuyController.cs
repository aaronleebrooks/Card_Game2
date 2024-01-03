using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuyController : MonoBehaviour
{
    public UnityEvent<int> BuyChangedEvent = new UnityEvent<int>();
    private int buy;

    public void DoBuy()
    {
        Debug.Log("BuyController: Called DoBuy");
        ModifyBuy(-1);
    }

    public void DoGainBuy(int value)
    {
        Debug.Log($"BuyController: Called DoGainBuy with value {value}");
        ModifyBuy(value);
    }

    private void ModifyBuy(int value)
    {
        Debug.Log($"BuyController: Called ModifyBuy with value {value}");
        buy += value;
        BuyChangedEvent.Invoke(buy);
    }

    public void SetBuy(int value)
    {
        Debug.Log($"BuyController: Called SetBuy with value {value}");
        buy = value;
        BuyChangedEvent.Invoke(buy);
    }
}