using System.Collections;
// using System.Collections.Generic;
using System;
// using TMPro;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AiBlindState : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.Blind;
    }
    public void Enter(AiAgent agent)
    {
        agent.navMeshAgent.speed = 0;
        agent.animator.SetLayerWeight(2,1);
        agent.RigLayer.GetComponent<Rig>().weight = 0;
        agent.Weapon.SetActive(false);
        agent.Blind = true;

        agent.StartCoroutine(TimeBlind(agent));
    }
    public void Update(AiAgent agent)
    {
    }
    public void Exit(AiAgent agent)
    {
    }
    IEnumerator TimeBlind(AiAgent agent)
    {
        yield return new WaitForSeconds(5);
        agent.navMeshAgent.speed = 7;
        agent.animator.SetLayerWeight(2,0);
        agent.RigLayer.GetComponent<Rig>().weight = 1;
        agent.Weapon.SetActive(true);
        agent.initialState = AiStateId.ChasePlayer;
        agent.Blind = false;
    }
}