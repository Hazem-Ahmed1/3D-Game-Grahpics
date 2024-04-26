using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStealState : AiState
{
    public float detectionRange = 5f; // The distance within which the enemy can detect the player
    public float detectionDuration = 2f; // The time (in seconds) the player must remain within the detection range

    private float detectionTimer = 0f; // The timer for tracking the detection duration
    private float preSpeed = 0f;
    public AiStateId GetId()
    {
        return AiStateId.StealKey;
    }
    public void Enter(AiAgent agent)
    {
        preSpeed = agent.navMeshAgent.speed;
        agent.navMeshAgent.speed = 0;
    }
    public void Update(AiAgent agent)
    {
            // Calculate the distance between the enemy and the player
            float distance = Vector3.Distance(agent.transform.position, agent.PlayerTransform.transform.position);

            // If the player is within the detection range
            if (distance <= detectionRange)
            {
                // Increment the detection timer
                detectionTimer += Time.deltaTime;

                // If the player has remained within the detection range for the required duration
                if (detectionTimer >= detectionDuration)
                {
                    agent.keyInventory.hasDoorLockedKey = false;
                    agent.hasDoorLockedKey = true;
                    // Debug.Log("Player is near the enemy for at least 3 seconds.");
                }
            }
            else
            {
                // Reset the detection timer
                detectionTimer = 0f;
            }
    }
    public void Exit(AiAgent agent)
    {
        agent.navMeshAgent.speed = preSpeed;
    }
}
