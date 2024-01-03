using UnityEngine;

[CreateAssetMenu(menuName="Card/Generic")]
public class SO_Card : ScriptableObject
{
    public int typeId;
    public Sprite cardModel;
    public int cost;
    public int power;
    public string title;
    public string description;
    public string flavor;
    public Sprite background;
}