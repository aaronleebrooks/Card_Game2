using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CreatureController : MonoBehaviour
{
    public UnityEvent<int> SetUpCardAttack = new UnityEvent<int>();
    public UnityEvent<int> SetUpCardHealth = new UnityEvent<int>();

    public void SetUpCreature(Card card)
    {
        GetCardSprite(card);
        SetUpCardAttack.Invoke(GetCardAttack(card));
        SetUpCardHealth.Invoke(GetCardHealth(card));
    }
    private void GetCardSprite(Card card)
    {
        GetComponent<SpriteRenderer>().sprite = card.GetCardSprite();
    }

private int GetCardAttack(Card card)
{
    HealthController healthController = card.GetComponentInChildren<HealthController>();
    if (healthController == null)
    {
        Debug.LogError("GetCardAttack: HealthController is null");
        return 0;
    }

    return healthController.GetMaxHealth();
}

private int GetCardHealth(Card card)
{
    HealthController healthController = card.GetComponentInChildren<HealthController>();
    if (healthController == null)
    {
        Debug.LogError("GetCardHealth: HealthController is null");
        return 0;
    }

    return healthController.GetMaxHealth();
}
}
