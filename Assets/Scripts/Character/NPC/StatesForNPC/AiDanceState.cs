using System.Collections;
// using System.Collections.Generic;
using System;
// using TMPro;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AiDanceState : AiState
{
    private float danceStartTime;
    public AiStateId GetId()
    {
        return AiStateId.Dance;
    }
    public void Enter(AiAgent agent)
    {
        agent.navMeshAgent.speed = 0;
        agent.animator.SetLayerWeight(1,1);
        agent.RigLayer.GetComponent<Rig>().weight = 0;
        agent.Weapon.SetActive(false);
        agent.dance = true;

        danceStartTime = Time.time;
        agent.StartCoroutine(TimeDance(agent));
    }
    public void Update(AiAgent agent)
    {
    }

    public void Exit(AiAgent agent)
    {
    }
    IEnumerator TimeDance(AiAgent agent)
    {
        yield return new WaitForSeconds(5);
        agent.navMeshAgent.speed = 7;
        agent.animator.SetLayerWeight(1,0);
        agent.RigLayer.GetComponent<Rig>().weight = 1;
        agent.Weapon.SetActive(true);
        agent.initialState = AiStateId.ChasePlayer;
        agent.dance = false;
    }
}

