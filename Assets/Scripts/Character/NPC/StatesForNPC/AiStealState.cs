using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class AiStealState : AiState
{
    public float detectionRange = 5f; // The distance within which the enemy can detect the player
    public float detectionDuration = 5f; // The time (in seconds) the player must remain within the detection range

    private float detectionTimer = 0f; // The timer for tracking the detection duration
    // private float preSpeed = 0f;
    // private TimerKey timerKey;
    private bool inRange = false;
    
    public int Duration;
    private int remainingDuration = 5;
    public AiStateId GetId()
    {
        return AiStateId.StealKey;
    }
    public void Enter(AiAgent agent)
    {
        agent.navMeshAgent.stoppingDistance = 3;
        // agent.navMeshAgent.speed = 0;
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
                if (!inRange)
                {
                    Duration = 5;
                    remainingDuration = 5;
                    inRange = true;
                    agent.StartCoroutine(UpdateTimer(agent));
                }
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
                Debug.Log("out");
                detectionTimer = 0f;
                inRange = false;
            }
    }
    public void Exit(AiAgent agent)
    {
    }

private IEnumerator UpdateTimer(AiAgent agent)
    {
        while(remainingDuration >= 0) 
        {
            agent.UIFill.fillAmount = Mathf.InverseLerp(0,Duration,remainingDuration);
            remainingDuration--;
            yield return new WaitForSeconds(1f);
            if (!inRange)
            {
                agent.UIFill.fillAmount = 1f;
                yield break;
            }
        }
    }
}
