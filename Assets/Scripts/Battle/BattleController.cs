    using System; // Add the missing import statement
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleController : MonoBehaviour
{
    public SO_Battle battle;
    public PlayerController player;
    public PlayerController enemy;
    public UnityEvent<SO_Deck> InitStoreEvent = new UnityEvent<SO_Deck>();
    public UnityEvent InitPlayerEvent = new UnityEvent();
    public UnityEvent PlayerTurnStartEvent = new UnityEvent();
    public UnityEvent EnemyTurnStartEvent = new UnityEvent();
    public UnityEvent PlayerTurnMainEvent = new UnityEvent();
    public UnityEvent EnemyTurnMainEvent = new UnityEvent();
    public UnityEvent PlayerTurnEndEvent = new UnityEvent();
    public UnityEvent EnemyTurnEndEvent = new UnityEvent();

    public enum TurnPhase
    {
        StartPlayer,
        MainPlayer,
        EndPlayer,
        StartEnemy,
        MainEnemy,
        EndEnemy,
    }

    private TurnPhase currentTurnPhase;

    private void Start()
    {
        Debug.Log("Battle started");
        InitializeBattle();
    }

    private void InitializeBattle()
    {
        Debug.Log("Battle initialized");
        InitPlayerEvent.Invoke();
        InitStoreEvent.Invoke(battle.storeDeck);
    }

    public void TriggerNextTurnPhase()
    {
        Debug.Log("TriggerNextTurnPhase called");
        NextTurnPhase();
    }

    private void NextTurnPhase()
    {
        currentTurnPhase = (TurnPhase)(((int)currentTurnPhase + 1) % Enum.GetValues(typeof(TurnPhase)).Length);
        Debug.Log($"Turn phase changed to: {currentTurnPhase}");
        switch (currentTurnPhase)
        {
            case TurnPhase.StartPlayer:
                Debug.Log("Player turn started");
                PlayerTurnStartEvent.Invoke();
                break;
            case TurnPhase.MainPlayer:
                Debug.Log("Player main phase");
                PlayerTurnMainEvent.Invoke();
                break;
            case TurnPhase.EndPlayer:
                Debug.Log("Player turn ended");
                PlayerTurnEndEvent.Invoke();
                break;
            case TurnPhase.StartEnemy:
                Debug.Log("Enemy turn started");
                EnemyTurnStartEvent.Invoke();
                break;
            case TurnPhase.MainEnemy:
                Debug.Log("Enemy main phase");
                EnemyTurnMainEvent.Invoke();
                break;
            case TurnPhase.EndEnemy:
                Debug.Log("Enemy turn ended");
                EnemyTurnEndEvent.Invoke();
                break;
        }
    }
}