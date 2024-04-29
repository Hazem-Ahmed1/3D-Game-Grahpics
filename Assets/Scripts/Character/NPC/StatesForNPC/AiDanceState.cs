using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AiDanceState : AiState
{
    // private float danceStartTime;
    public AiStateId GetId()
    {
        return AiStateId.Dance;
    }
    public void Enter(AiAgent agent)
    {
        // Debug.Log("llll");
        if (!agent.snatcher)
        {
            agent.navMeshAgent.speed = 0;
            agent.animator.SetLayerWeight(1,1);
            agent.RigLayer.GetComponent<Rig>().weight = 0;
            agent.Weapon.SetActive(false);
            agent.dance = true;
        }else
        {
            agent.navMeshAgent.speed = 0;
            agent.animator.SetLayerWeight(1,1);
            // agent.RigLayer.GetComponent<Rig>().weight = 0;
            // agent.Weapon.SetActive(false);
            agent.dance = true;
        }
        

        // danceStartTime = Time.time;
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
        if (!agent.snatcher)
        {
            agent.navMeshAgent.speed = 7;
            agent.animator.SetLayerWeight(1,0);
            agent.RigLayer.GetComponent<Rig>().weight = 1;
            agent.Weapon.SetActive(true);
            agent.initialState = AiStateId.ChasePlayer;
            agent.dance = false;
        }else{
            agent.navMeshAgent.speed = 7;
            agent.animator.SetLayerWeight(1,0);
            // agent.RigLayer.GetComponent<Rig>().weight = 1;
            // agent.Weapon.SetActive(true);
            agent.initialState = AiStateId.ChasePlayer;
            agent.dance = false;
        }
    }
}