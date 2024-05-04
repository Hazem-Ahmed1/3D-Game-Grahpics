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
            // Debug.Log("88888");
            return;
        }
        timer -= Time.deltaTime;
        if (!agent.navMeshAgent.hasPath)
        {
            if (agent.KeyTransform != null){
                agent.navMeshAgent.destination = agent.KeyTransform.transform.position;
            }
        }
        if (timer < 0.0f)
        {
            if (agent.KeyTransform != null)
            {
                // Debug.Log("88888");
                Vector3 direction = (agent.KeyTransform.transform.position - agent.navMeshAgent.destination);
                direction.y = 0;
                if (direction.sqrMagnitude > agent.config.maxDistance * agent.config.maxDistance)
                {
                    if (agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                    {
                        agent.navMeshAgent.destination = agent.KeyTransform.transform.position;
                        // Debug.Log("88888");
                    }
                }
                timer = agent.config.maxTime;
            }
            else
            {
                return;
            }
        }
    }

    public void Exit(AiAgent agent)
    {
    }
}
