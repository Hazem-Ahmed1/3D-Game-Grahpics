using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    public AiStateMachine stateMachine;
    public AiStateId initialState;
    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    public AiAgentConfig config;


    [HideInInspector] public Transform PlayerTransform;
    [HideInInspector] public Transform KeyTransform;
    [HideInInspector] public Transform FinalGoalTransform;
    // public AiLocomotion aiLocomotion;
    public GameObject Weapon;
    // public GameObject NPC;
    public GameObject RigLayer;
    public Animator animator;
    public bool hasDoorLockedKey = false;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = new AiStateMachine(this);
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        FinalGoalTransform = GameObject.FindGameObjectWithTag("DoorGoal").transform;
        KeyTransform = GameObject.Find("KeyDoor").transform;

        // Create instance from this state
        stateMachine.RegisterState(new AiGetKeyState());
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiGoToFinalGoalState());
        stateMachine.RegisterState(new AiDanceState());
        // stateMachine.RegisterState(new AiDeathState());
        initialState = AiStateId.goToKey;
        stateMachine.changeState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasDoorLockedKey)
        {
            // Debug.Log("Bedo");
            initialState = AiStateId.goToFinalGoal;
            stateMachine.changeState(initialState);
        }
        stateMachine.changeState(initialState);
        stateMachine.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("1");
        if (other.gameObject.CompareTag("DoorInteractiveObj"))
        {
            // Debug.Log("Bedo");
            hasDoorLockedKey = true;
            Destroy(other.gameObject);
        }
    }
}