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
    public KeyDoorController keyDoorController;

    [HideInInspector] public Transform PlayerTransform;
    [HideInInspector] public Transform KeyTransform;
    [HideInInspector] public Transform FinalGoalTransform;
    // public AiLocomotion aiLocomotion;
    public GameObject Weapon;
    // public GameObject NPC;
    public GameObject RigLayer;
    public Animator animator;
    public bool hasDoorLockedKey = false;
    public bool IsOpen = false;
    private float distancePlayer;

    // for weapon
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = new AiStateMachine(this);
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        FinalGoalTransform = GameObject.Find("LockedDoor").transform;
        KeyTransform = GameObject.Find("KeyDoor").transform;
        // keyDoorController = GameObject.Find("LockedDoor").GetComponent<KeyDoorController>();

        // Create instance from this state
        stateMachine.RegisterState(new AiGetKeyState());
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiGoToFinalGoalState());
        stateMachine.RegisterState(new AiDanceState());
        stateMachine.RegisterState(new AiAttackPlayerState());
        // stateMachine.RegisterState(new AiDeathState());
        initialState = AiStateId.goToKey;
        stateMachine.changeState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        distancePlayer = Vector3.Distance(PlayerTransform.position, this.gameObject.transform.position);
        if (!hasDoorLockedKey)
        {
            initialState = AiStateId.goToKey;
        }
        if (hasDoorLockedKey)
        {
            initialState = AiStateId.goToKey;
        }
        if (distancePlayer <= 15f && !hasDoorLockedKey)
        {
            initialState = AiStateId.AttackPlayer;
            // StartCoroutine(Shot());
        }
        stateMachine.changeState(initialState);
        stateMachine.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DoorInteractiveObj"))
        {
            hasDoorLockedKey = true;
            initialState = AiStateId.goToFinalGoal;
            stateMachine.changeState(initialState);
            Destroy(other.gameObject);
            navMeshAgent.stoppingDistance = 0;
        }
        else if (other.gameObject.CompareTag("DoorGoal") && hasDoorLockedKey && !IsOpen)
        {
            keyDoorController.OpenDoor();
            navMeshAgent.stoppingDistance = 1.5f;
            initialState = AiStateId.ChasePlayer;
            stateMachine.changeState(initialState);
            Physics.IgnoreLayerCollision(3,7);
            IsOpen = true;
        }
        else if (other.gameObject.CompareTag("Bullet_Dancing"))
        {
            initialState = AiStateId.Dance;
            stateMachine.changeState(initialState);
        }
    }
}