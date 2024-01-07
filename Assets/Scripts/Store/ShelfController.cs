using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShelfController : MonoBehaviour
{
    public Card shelfCard;
    public int typeId;
    private int stock;
    private int price;
    public Button BuyButton;
    public GameObject SoldOutSign;
    public CardPositionController cardPositionController;
    public UnityEvent<int> StockChangedEvent = new UnityEvent<int>();
    public bool isMasked = true;
    public Mask mask;

    public void ToggleMask(bool value)
    {
        if (mask != null)
        {
            Debug.Log($"ToggleMask: Toggling mask to {!value}");
            mask.enabled = !value;
            shelfCard.ToggleCardHidden(!value);
        }
        else
        {
            Debug.LogError("ToggleMask: Mask is null");
        }
    }

    public void InitializeShelfCard(Card card, int startingStock)
    {
        Debug.Log($"InitializeShelfCard: Initializing shelf card {card.name} with starting stock {startingStock}");
        if (card == null)
        {
            Debug.LogError("InitializeShelfCard: Card is null");
            return;
        }
        shelfCard = card;
        stock = startingStock;
        price = card.cost;
        card.SetSortingLayer("Store");
        card.SetSortingOrder(10);
        BuyButton.interactable = false;
        SoldOutSign.SetActive(false);
        StockChangedEvent.Invoke(stock);
        card.SetParent(cardPositionController.transform);
        cardPositionController.OnMoveCardToThisPosition(card);
        card.ToggleCardHidden(true);
    }

    private void ModifyStock(int value)
    {
        Debug.Log($"ModifyStock: Modifying stock by {value}");
        stock += value;
        StockChangedEvent.Invoke(stock);
        if (stock < 0)
        {
            Debug.Log("ModifyStock: Stock is less than 0, setting to 0");
            stock = 0;
        }
    }

    public void DoBuy()
    {
        Debug.Log("DoBuy: Buying item, reducing stock by 1");
        ModifyStock(-1);
    }

    public void SetTypeId(int value)
    {
        Debug.Log($"SetTypeId: Setting typeId to {value}");
        typeId = value;
    }

    public void CheckIfCanBuy(int mana)
    {
        Debug.Log($"CheckIfCanBuy: Checking if can buy, mana is {mana}, price is {price}");
        if (mana >= price && stock > 0)
        {
            Debug.Log("CheckIfCanBuy: Can buy, setting buy button enabled");
            SetBuyEnabled(true);
        }
        else
        {
            Debug.Log("CheckIfCanBuy: Cannot buy, setting buy button disabled");
            SetBuyEnabled(false);
        }
    }
    public void SetBuyEnabled(bool value)
    {
        Debug.Log($"SetBuyEnabled: Setting buy button enabled to {value}");
        BuyButton.interactable = value;
    }
}
