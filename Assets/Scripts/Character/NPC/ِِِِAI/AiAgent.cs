using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public AiStateMachine stateMachine;
    public AiStateId initialState;
    [HideInInspector]
    public NavMeshAgent navMeshAgent ;
    public AiAgentConfig config;

    [SerializeField] public Transform PlayerTransform;
    [SerializeField] public Transform KeyTransform;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = new AiStateMachine(this);
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerTransform = GameObject.FindGameObjectWithTag("Key").transform;
        // Create instance from this state
        stateMachine.RegisterState(new AiGetKeyState());
        stateMachine.RegisterState(new AiChasePlayerState());
        // stateMachine.RegisterState(new AiDeathState());

        stateMachine.changeState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}