using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfController : MonoBehaviour
{
    public Card shelfCard;
    public int typeId;
    private int stock;
    private int price;
    public GameObject BuyButton;
    public GameObject SoldOutSign;

    public void InitializeShelfCard(Card card, int startingStock)
    {
        shelfCard = card;
        stock = startingStock;
        price = card.cost;
        SoldOutSign.SetActive(false);
    }

    private void ModifyStock(int value)
    {
        stock += value;
        if (stock < 0)
        {
            stock = 0;
        }
    }

    private void IsEmpty()
    {

    }

    private void DisableBuyButton()
    {

    }

    private void EnableBuyButton()
    {

    }

    public void DoBuy()
    {
        ModifyStock(-1);
    }
}
