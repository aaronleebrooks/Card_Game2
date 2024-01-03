using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    public UnityEvent<int> HealthChangedEvent = new UnityEvent<int>();
    private int health;
    private int maxHealth;
    private bool canBeOverHealed = false;

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void DoDamage(int damage)
    {
        ModifyHealth(-damage);
    }

    public void DoHeal(int heal)
    {
        ModifyHealth(heal);
    }

    public void SetOverheal(bool value)
    {
        canBeOverHealed = value;
    }

    private void ModifyHealth(int value)
    {
        health += value;

        if (health > maxHealth && !canBeOverHealed)
        {
            health = maxHealth;
        }
        HealthChangedEvent.Invoke(health);
    }

    public void SetHealth(int value)
    {
        health = value;
        maxHealth = value;
        HealthChangedEvent.Invoke(health);
    }

    public void DoInitializeCard(SO_Card card)
    {
        if(card is SO_CreatureCard)
        {
            SetHealth((card as SO_CreatureCard).health);
        }
    }

    public void DoInitializePlayer(SO_Player player)
    {
        SetHealth(player.health);
    }
}