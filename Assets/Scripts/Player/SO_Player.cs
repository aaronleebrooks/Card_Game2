using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "Player")]
public class SO_Player : ScriptableObject
{
    public SO_Deck deck;
    public int health;
    public Sprite sprite;
}