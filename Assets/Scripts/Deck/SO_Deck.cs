using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDeck", menuName = "Card/Deck")]
public class SO_Deck : ScriptableObject
{
    public List<SO_Card> cards = new List<SO_Card>();
    public Sprite cardBack;
}