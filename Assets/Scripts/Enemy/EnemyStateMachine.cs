using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine 
{
    public EnemyState currentEnemyState { get; private set; }
    
    public void Initialize(EnemyState startingState)
    {
        currentEnemyState = startingState;
        currentEnemyState.Enter();
    }
    public void ChangeState(EnemyState newState)
    {
        currentEnemyState.Exit();
        currentEnemyState = newState;
        currentEnemyState.Enter();
    }
    
}
