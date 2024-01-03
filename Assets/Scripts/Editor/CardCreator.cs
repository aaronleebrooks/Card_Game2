using UnityEngine;
using UnityEditor;

public class CardCreator : EditorWindow
{
    private int nextTypeId = 100001;
    private bool isCreature;

        // Common fields
    private Sprite cardModel;
    private int cost;
    private int power;
    private string cardTitle;
    private string description = "";
    private string flavor = "";
    private Sprite background;

    // Creature card specific fields
    private Sprite creatureModel;
    private int health;
    private int attack;

    [MenuItem("Window/Card Creator")]
    public static void ShowWindow()
    {
        CardCreator window = GetWindow<CardCreator>("Card Creator");
        string[] guids = AssetDatabase.FindAssets("t:SO_Card", new[] { "Assets/ScriptableObjects/Cards" });
        window.nextTypeId = 100001 + guids.Length;
    }

private void OnGUI()
{
    // Common fields
    cardModel = (Sprite)EditorGUILayout.ObjectField("Card Model", cardModel, typeof(Sprite), false);
    cost = EditorGUILayout.IntField("Cost", cost);
    power = EditorGUILayout.IntField("Power", power);
    cardTitle = EditorGUILayout.TextField("cardTitle", cardTitle);
    description = EditorGUILayout.TextField("Description", description);
    flavor = EditorGUILayout.TextField("Flavor", flavor);
    background = (Sprite)EditorGUILayout.ObjectField("Background", background, typeof(Sprite), false);
    creatureModel = (Sprite)EditorGUILayout.ObjectField("Creature Model", creatureModel, typeof(Sprite), false);
    health = EditorGUILayout.IntField("Health", health);
    attack = EditorGUILayout.IntField("Attack", attack);

    // Spacer
    EditorGUILayout.Space();

    // Creature card specific fields
    if (GUILayout.Button("Create Creature Card"))
    {


        CreateCreatureCard();
    }

    // Spell card specific fields
    if (GUILayout.Button("Create Spell Card"))
    {
        CreateSpellCard();
    }
}

    private void CreateCreatureCard()
    {
        SO_CreatureCard creatureCard = ScriptableObject.CreateInstance<SO_CreatureCard>();
        CreateCard(creatureCard);
    }

    private void CreateSpellCard()
    {
        SO_SpellCard spellCard = ScriptableObject.CreateInstance<SO_SpellCard>();
        CreateCard(spellCard);
    }

    private void CreateCard(SO_Card card)
    {
        card.typeId = nextTypeId++;
        card.cardModel = cardModel;
        card.cost = cost;
        card.power = power;
        card.title = cardTitle;
        card.description = description;
        card.flavor = flavor;
        card.background = background;

        if (card is SO_CreatureCard creatureCard)
        {
            creatureCard.creatureModel = creatureModel;
            creatureCard.health = health;
            creatureCard.attack = attack;
        }
        string filename = card.title.Replace(" ", "");
        AssetDatabase.CreateAsset(card, "Assets/ScriptableObjects/Cards/" + filename + ".asset");
        AssetDatabase.SaveAssets();
    }
}