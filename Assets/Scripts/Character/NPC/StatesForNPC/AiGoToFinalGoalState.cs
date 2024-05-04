using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiGoToFinalGoalState : AiState
{
    float timer = 0.0f;
    public AiStateId GetId()
    {
        return AiStateId.goToFinalGoal;
    }
    public void Enter(AiAgent agent)
    {
        if (!agent.navMeshAgent.hasPath)
        {
            if (agent.GoalTransform != null && agent.snatcher){
                agent.navMeshAgent.destination = agent.GoalTransform.position;
            }
            else if (agent.FinalGoalTransform != null && !agent.snatcher){
                agent.navMeshAgent.destination = agent.FinalGoalTransform.position;
            }
        }
    }
    public void Update(AiAgent agent)
    {
        if (!agent.enabled)
        {
            return;
        }
        timer -= Time.deltaTime;
        
        if (agent.GoalTransform != null && agent.snatcher){
            if (timer < 0.0f)
            {
                Vector3 direction = agent.GoalTransform.position - agent.navMeshAgent.destination;
                direction.y = 0;
                if (direction.sqrMagnitude > agent.config.maxDistance * agent.config.maxDistance)
                {
                    if (agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                    {
                        agent.navMeshAgent.destination = agent.GoalTransform.position;
                    }
                }
                timer = agent.config.maxTime;
            }
        }
        else if (agent.FinalGoalTransform != null && !agent.snatcher)
        {
            if (timer < 0.0f)
            {
                Vector3 direction = agent.FinalGoalTransform.position - agent.navMeshAgent.destination;
                direction.y = 0;
                if (direction.sqrMagnitude > agent.config.maxDistance * agent.config.maxDistance)
                {
                    if (agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                    {
                        agent.navMeshAgent.destination = agent.FinalGoalTransform.position;
                    }
                }
                timer = agent.config.maxTime;
            }
        }else
        {
            return;
        }
    }
    public void Exit(AiAgent agent)
    {
    }
}
