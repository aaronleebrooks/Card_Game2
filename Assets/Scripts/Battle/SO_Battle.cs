using UnityEngine;

[CreateAssetMenu(fileName = "NewBattle", menuName = "Battle")]
public class SO_Battle : ScriptableObject
{
    public SO_Player enemy;
    public SO_Deck storeDeck;
}