using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AiStateId{
    goToKey,
    ChasePlayer,
    AttackPlayer,
    goToDoorGoal,
    goToFinalGoal,
    Dance,
    Blind,
    Freeze,
    StealKey
}
public interface AiState
{
    AiStateId GetId();
    void Enter(AiAgent agent);
    void Update(AiAgent agent);
    void Exit(AiAgent agent);
}
