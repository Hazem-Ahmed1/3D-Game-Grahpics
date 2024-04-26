using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AiFreezeState : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.Freeze;
    }
    public void Enter(AiAgent agent)
    {
        agent.navMeshAgent.speed = 0;
        // agent.animator.SetLayerWeight(2,1);
        agent.RigLayer.GetComponent<Rig>().weight = 0;
        agent.Weapon.SetActive(false);
        agent.Blind = true;

        agent.StartCoroutine(FreezeTime(agent));
    }
    public void Update(AiAgent agent)
    {
        
    }
    public void Exit(AiAgent agent)
    {
        
    }
    IEnumerator FreezeTime(AiAgent agent)
    {
        yield return new WaitForSeconds(5);
        agent.navMeshAgent.speed = 7;
        // agent.animator.SetLayerWeight(2,0);
        agent.RigLayer.GetComponent<Rig>().weight = 1;
        agent.Weapon.SetActive(true);
        agent.initialState = AiStateId.ChasePlayer;
        agent.Blind = false;
    }
}
