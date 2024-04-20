using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiLocomotion : MonoBehaviour
{
    [SerializeField] private Transform PlayerTransform;
    // for optimize the movement
    // [SerializeField] private float maxTime = 1.0f;
    // [SerializeField] private float maxDistance = 1.0f;
    // float timer = 0.0f;
    NavMeshAgent agent;
    Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // timer -= Time.deltaTime;
        // if (timer < 0.0f)
        // {
        //     float sqrDistance = (PlayerTransform.position - agent.destination).sqrMagnitude;
        //     if (sqrDistance > maxDistance*maxDistance)
        //     {
        //         agent.destination = PlayerTransform.position;
        //     }
        //     timer = maxTime;
        // }
        agent.destination = PlayerTransform.position;
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
