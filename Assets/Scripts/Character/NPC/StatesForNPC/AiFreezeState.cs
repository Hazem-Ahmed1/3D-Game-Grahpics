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
        if (!agent.snatcher){
            agent.navMeshAgent.speed = 0;
            agent.RigLayer.GetComponent<Rig>().weight = 0;
            agent.Weapon.SetActive(false);
            agent.Blind = true;
        }else{
            agent.navMeshAgent.speed = 0;
            // agent.RigLayer.GetComponent<Rig>().weight = 0;
            // agent.Weapon.SetActive(false);
            agent.Blind = true;
        }

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
        
        if (!agent.snatcher){
            agent.navMeshAgent.speed = 7;
            agent.RigLayer.GetComponent<Rig>().weight = 1;
            agent.Weapon.SetActive(true);
            agent.Blind = false;
        }else{
            agent.navMeshAgent.speed = 7;
            // agent.RigLayer.GetComponent<Rig>().weight = 0;
            // agent.Weapon.SetActive(false);
            agent.Blind = false;
        }
    }
}
