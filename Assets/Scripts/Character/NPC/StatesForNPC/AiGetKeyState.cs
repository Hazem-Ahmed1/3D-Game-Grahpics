using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiGetKeyState : AiState
{
    float timer = 0.0f;
    public AiStateId GetId()
    {
        return AiStateId.goToKey;
    }
    public void Enter(AiAgent agent)
    {
    }
    public void Update(AiAgent agent)
    {
        if (!agent.enabled)
        {
            return;
        }
        timer -= Time.deltaTime;
        if (!agent.navMeshAgent.hasPath)
        {
            agent.navMeshAgent.destination = agent.KeyTransform.position;
        }
        if (timer < 0.0f)
        {
            Vector3 direction = (agent.KeyTransform.position - agent.navMeshAgent.destination);
            direction.y = 0;
            if (direction.sqrMagnitude > agent.config.maxDistance * agent.config.maxDistance)
            {
                if (agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                {
                    agent.navMeshAgent.destination = agent.KeyTransform.position;
                }
            }
            
            timer = agent.config.maxTime;
        }
    }

    public void Exit(AiAgent agent)
    {
    }
}
