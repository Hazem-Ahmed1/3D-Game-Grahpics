using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class AiAgent : MonoBehaviour
{
    public AiStateMachine stateMachine;
    public AiStateId initialState;
    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    public AiAgentConfig config;

    public KeyDoorController keyDoorController;
    public DoorController DoorController;
    public KeyInventory keyInventory;
    public GameObject KeyFlag;
    public GameObject BodyLight;
    public bool snatcher = false;

    [HideInInspector] public GameObject PlayerTransform;
    [HideInInspector] public GameObject KeyTransform;
    [HideInInspector] public Transform DoorKeyTransform;
    [HideInInspector] public Transform FinalGoalTransform;
    [HideInInspector] public Animator animator;
    public GameObject Weapon;
    public GameObject RigLayer;
    public bool hasDoorLockedKey = false;
    public bool IsOpen = false;
    public bool dance = false;
    public bool Blind = false;
    public bool freeze = false;
    private float distancePlayer;

    // for weapon
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        stateMachine = new AiStateMachine(this);
        PlayerTransform = GameObject.FindGameObjectWithTag("Player");
        DoorKeyTransform = GameObject.Find("LockedDoor").transform;
        FinalGoalTransform = GameObject.Find("FinalGoal").transform;

        // Create instance from this state
        stateMachine.RegisterState(new AiGetKeyState());
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiGoToFinalGoalState());
        stateMachine.RegisterState(new AiGoToDoorGoalState());
        stateMachine.RegisterState(new AiDanceState());
        stateMachine.RegisterState(new AiBlindState());
        stateMachine.RegisterState(new AiFreezeState());
        stateMachine.RegisterState(new AiAttackPlayerState());
        stateMachine.RegisterState(new AiStealState());
        initialState = AiStateId.goToKey;
        stateMachine.changeState(initialState);
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        
        if (GameObject.Find("FinalGoal") != null)
        {
            FinalGoalTransform = GameObject.Find("FinalGoal").transform;
        }
        KeyTransform = GameObject.Find("KeyDoor");
        // if (hasDoorLockedKey)
        // {
        //     KeyFlag.active =true;
        //     BodyLight.active = true;
        // }
        // else if(!hasDoorLockedKey)
        // {
        //     KeyFlag.active = false;
        //     BodyLight.active = false;
        // }
        // KeyFlag.active = hasDoorLockedKey? true : false;
        // BodyLight.active = hasDoorLockedKey? true : false;
        KeyFlag.active = (hasDoorLockedKey)? true : false;
        Debug.Log(hasDoorLockedKey);
        BodyLight.active = (hasDoorLockedKey)? true : false;
        distancePlayer = Vector3.Distance(PlayerTransform.transform.position, this.gameObject.transform.position);
        float layerWeight = PlayerTransform.GetComponentInChildren<Animator>().GetLayerWeight(3);

        if (!hasDoorLockedKey && KeyTransform != null && !snatcher)
        {
            initialState = AiStateId.goToKey;
        }
        if (hasDoorLockedKey && !snatcher)
        {
            initialState = AiStateId.goToDoorGoal;
            
        }
        if (distancePlayer <= 10f && !hasDoorLockedKey && !dance && !Blind && !freeze && !snatcher)
        {
            initialState = AiStateId.AttackPlayer;
        }
        if (keyInventory.hasDoorLockedKey && distancePlayer >= 3f && !snatcher)
        {
            initialState = AiStateId.ChasePlayer;
        }
        if (keyInventory.hasDoorLockedKey && distancePlayer < 5f && !snatcher)
        {
            initialState = AiStateId.StealKey;
        }
        if (snatcher)
        {
            if (FinalGoalTransform !=null)
            {
                initialState = AiStateId.goToFinalGoal;
            }
        }
        if (navMeshAgent.hasPath)
        {
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
        }else{
            animator.SetFloat("Speed",0);
        }
        stateMachine.changeState(initialState);
        stateMachine.Update();
    }

    // [System.Obsolete]
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("KeyDoor"))
        {
            hasDoorLockedKey = true;
            initialState = AiStateId.goToDoorGoal;
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
            IsOpen = true;
        }
        else if (other.gameObject.CompareTag("DoorSnatch") && !IsOpen && snatcher)
        {
            DoorController.PlayAnimation();
            initialState = AiStateId.goToFinalGoal;
            stateMachine.changeState(initialState);
            IsOpen = true;
        }
        else if (other.gameObject.CompareTag("FinalGoal") && snatcher)
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Bullet_Dancing"))
        {
            initialState = AiStateId.Dance;
            stateMachine.changeState(initialState);
        }
        else if (other.gameObject.CompareTag("Blind_Bullet"))
        {
            initialState = AiStateId.Blind;
            stateMachine.changeState(initialState);
        }
        else if (other.gameObject.CompareTag("BulletTornado"))
        {
            initialState = AiStateId.Freeze;
            stateMachine.changeState(initialState);
        }
    }
}