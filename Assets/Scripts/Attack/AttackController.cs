using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackController : MonoBehaviour
{
    public UnityEvent<int> AttackChangedEvent = new UnityEvent<int>();
    public UnityEvent<bool> IsAbleToAttackToggledEvent = new UnityEvent<bool>();
    private int attack;
    bool isAbleToAttack = false;

    public int GetAttack()
    {
        Debug.Log("AttackController: Called GetAttack");
        return attack;
    }

    private void SetAttack(int value)
    {
        Debug.Log($"AttackController: Called SetAttack with value {value}");
        attack = value;
        AttackChangedEvent.Invoke(attack);
    }

    private void SetIsAbleToAttack(bool value)
    {
        Debug.Log($"AttackController: Called SetIsAbleToAttack with value {value}");
        isAbleToAttack = value;
        IsAbleToAttackToggledEvent.Invoke(isAbleToAttack);
    }

    public void DoCanAttackToggle()
    {
        Debug.Log("AttackController: Called DoCanAttackToggle");
        SetIsAbleToAttack(true);
    }

    public void DoChangeAttack(int value)
    {
        Debug.Log($"AttackController: Called DoChangeAttack with value {value}");
        SetAttack(attack + value);
    }

    public void DoSetAttack(int value)
    {
        Debug.Log($"AttackController: Called DoSetAttack with value {value}");
        SetAttack(value);
    }

    public void DoInitializeCard(SO_Card card)
    {
        Debug.Log($"AttackController: Called DoInitializeCard with card {card.name}");
        if(card is SO_CreatureCard)
        {
            SetAttack((card as SO_CreatureCard).attack);
        }
    }
}